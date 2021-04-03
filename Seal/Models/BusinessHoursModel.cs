using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Seal.Models
{
    public class BusinessHoursDTO
    {

        public string Day { get; set; }
        public string Opens { get; set; }
        public string Closes { get; set; }

        public BusinessHoursDTO(string day, TimeSpan opens, TimeSpan closes)
        {
            Opens = opens.ToString(@"hh\:mm");
            Closes = closes.ToString(@"hh\:mm");
            Day = day;
        }
    }

    public class BusinessHoursDTOs
    {

        public List<BusinessHoursDTO> Hours { get; set; }

        public BusinessHoursDTOs(ObservableCollection<BusinessHoursModel> businessHours)
        {
            Hours = new List<BusinessHoursDTO>();
            foreach (var model in businessHours)
            {
                foreach (var day in model.Days)
                {
                    if (day.Selected)
                    {
                        Hours.Add(new BusinessHoursDTO(day.Name, model.Opens, model.Closes));
                    }
                }
            }
        }
    }

    public class BusinessHoursModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<DayModel> Days { get; set; }
        public TimeSpan Opens { get; set; }
        public TimeSpan Closes { get; set; }

        public BusinessHoursModel()
        {
            Opens = new TimeSpan(09, 00, 00);
            Closes = new TimeSpan(20, 00, 00);
            Days = new ObservableCollection<DayModel>
            {
                new DayModel { Name = "Mon" },
                new DayModel { Name = "Tue" },
                new DayModel { Name = "Wed" },
                new DayModel { Name = "Thu" },
                new DayModel { Name = "Fri" },
                new DayModel { Name = "Sat" },
                new DayModel { Name = "Sun" }
            };
        }
    }

    public class DayModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }

        private bool selected;

        public bool Selected
        {
            get { return selected; }

            set
            {
                selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }
    }
}
