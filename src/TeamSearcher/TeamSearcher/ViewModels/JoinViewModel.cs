using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class JoinViewModel : ViewModelBase
{
    [ObservableProperty] [Required(ErrorMessage = "يجب إضافة اسم")] [NotifyDataErrorInfo]
    private string _name = string.Empty;
    [ObservableProperty] [Required(ErrorMessage = "مطلوب رقم الواتساب لكي يتواصل معك مالك التيم")] [PhoneNumber(ErrorMessage = "رقم الهاتف غير صحيح")] [NotifyDataErrorInfo]
    private string _number = string.Empty;
    [ObservableProperty]
    private bool _isMobile;

    private HttpClient client;

    public JoinViewModel()
    {
        client = new HttpClient();
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
        {
            IsMobile = MobileInputHelper.IsMobile();

            if (IsMobile)
            {
                MobileInputHelper.TextReceived += OnMobileTextReceived;
                MobileInputHelper.BackspacePressed += OnMobileBackspacePressed;
            }
        }
    }

    private void OnMobileTextReceived(string target, string text)
    {
        if (target == "Name") Name += text;
        else if (target == "Number") Number += text;
    }

    private void OnMobileBackspacePressed(string target)
    {
        if (target == "Name" && Name.Length > 0) Name = Name[..^1];
        else if (target == "Number" && Number.Length > 0) Number = Number[..^1];
    }

    // 3. Commands to trigger the keyboard
    [RelayCommand]
    private void FocusName() => MobileInputHelper.Focus("Name");

    [RelayCommand]
    private void FocusNumber() => MobileInputHelper.Focus("Number", "number");

    [RelayCommand]
    private async Task Search()
    {
        ValidateAllProperties();
        if (!HasErrors)
        {
            Person person = new Person{ Name = Name, Number = Number };
            
            var content = new StringContent(JsonSerializer.Serialize(person, AppJsonContext.Default.Person), Encoding.UTF8, "application/json");
            
            Console.WriteLine(await content.ReadAsStringAsync());
            
            HttpResponseMessage response =  await client.PostAsync("http://localhost:5213/api/v1/person/", content);
            
            if (response.IsSuccessStatusCode)
            {
                string idString = await response.Content.ReadAsStringAsync();
                
                if (int.TryParse(idString, out int id))
                {
                    WeakReferenceMessenger.Default.Send(new NavigationMessage(new TeamListViewModel(id)));
                }
            }
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage(new FirstViewModel()));
    }
}