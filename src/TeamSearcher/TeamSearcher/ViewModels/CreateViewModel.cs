using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TeamSearcher.Models;

namespace TeamSearcher.ViewModels;

public partial class CreateViewModel : ViewModelBase
{
    [ObservableProperty] [Required(ErrorMessage = "يجب إضافة اسم البروجيكت الذي يعمل عليه التيم")] [NotifyDataErrorInfo]
    private string _projectName;
    [ObservableProperty] [Required(ErrorMessage = "لا يمكن ترك الخانة فارغة")] private int? _currentCount = 1;
    [ObservableProperty] [Required(ErrorMessage = "لا يمكن ترك الخانة فارغة")] private int? _maxCount = 5;
    [ObservableProperty] private bool _isMobile;
    [ObservableProperty] private bool _boysOnly;
    [ObservableProperty] private bool _girlsOnly;
    [ObservableProperty] private string _errorMessage;
    private HttpClient client;

    public CreateViewModel()
    {
        if (OperatingSystem.IsBrowser() && MobileInputHelper.IsMobile())
        {
            IsMobile = true;
            MobileInputHelper.TextReceived += (target, text) => 
            {
                if (target == "Project") ProjectName += text;
            };

            MobileInputHelper.BackspacePressed += (target) => 
            {
                if (target == "Project" && ProjectName.Length > 0) 
                    ProjectName = ProjectName[..^1];
            };
        }

        client = new HttpClient();
    }

    [RelayCommand]
    private void FocusProject() => MobileInputHelper.Focus("Project");

    [RelayCommand]
    private async Task Search()
    {
        ErrorMessage = "";
        ValidateAllProperties();
        
        if (!HasErrors)
        {
            Team team = new Team
            {
                Name = ProjectName, 
                CurrentCount = (int)CurrentCount!, 
                MaxCount = (int)MaxCount!,
                Tag = (BoysOnly, GirlsOnly) switch
                {
                    (true, _) => Tag.BoysOnly,
                    (_, true) => Tag.GirlsOnly,
                    _ => Tag.None
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(team, AppJsonContext.Default.Team), Encoding.UTF8, "application/json");
            
            Console.WriteLine(await content.ReadAsStringAsync());

            HttpResponseMessage response = await client.PostAsync("http://localhost:5213/api/v1/team/", content);

            if (response.IsSuccessStatusCode)
            {
                string idString = await response.Content.ReadAsStringAsync();

                if (int.TryParse(idString, out int id))
                {
                    WeakReferenceMessenger.Default.Send(new NavigationMessage(new TeamDashViewModel(id)));
                }
            }
        }
        else if (MaxCount < CurrentCount)
        {
            ErrorMessage = "رقم الأعضاء الحالي يتعدي العدد الاقصى";
        }
        else if (MaxCount == CurrentCount && MaxCount != null && CurrentCount != null)
        {
            ErrorMessage = "التيم مكتمل بالفعل";
        }
    }

    [RelayCommand]
    private void RemoveTag()
    {
        BoysOnly = false;
        GirlsOnly = false;
    }
    
    [RelayCommand]
    private void GoBack()
    {
        WeakReferenceMessenger.Default.Send(new NavigationMessage(new FirstViewModel()));
    }
}