﻿using System;
using System.Linq;
using Geekhub.Modules.Core.Data;
using Geekhub.Modules.Core.Support;
using Geekhub.Modules.Meetings.Models;
using Geekhub.Modules.Meetings.ViewModels;

namespace Geekhub.Modules.Meetings.Data
{
    public class MeetingsService
    {
        private readonly MeetingFormModelBinder _meetingFormModelBinder = new MeetingFormModelBinder();

        public Meeting Create(MeetingFormModel formModel)
        {
            var meeting = new Meeting();

            SetID(meeting);

            _meetingFormModelBinder.FormModelToMeeting(formModel, meeting);

            DataContext.Current.Meetings.Add(meeting);

            return meeting;
        }

        public void Create(JsonViewModel.JsonMeetingViewModel formModel)
        {
            var meeting = new Meeting();

            SetID(meeting);

            //meeting.Address = formModel.Address;
            //meeting.AddressFormatted = formModel.Address;
            meeting.City = new City() { Name = formModel.City };
            meeting.CreatedAt = formModel.CreatedAt;
            meeting.Description = formModel.Description;
            //meeting.Latitude = formModel.Latitude;
            //meeting.Longtitude = formModel.Longtitude;
            meeting.Organizers.AddRange(formModel.Organizers.Select(x=>new Organizer() { Name = x}));
            meeting.StartsAt = formModel.StartsAt;
            meeting.Tags.AddRange(formModel.Tags.Select(x=>new Tag() { Name = x}));
            meeting.Title = formModel.Title;
            meeting.Url = formModel.Url;

            DataContext.Current.Meetings.Add(meeting);
        }

        public void Save(int meetingId, MeetingFormModel formModel)
        {
            var meeting = DataContext.Current.Meetings.FirstOrDefault(x => x.Id == meetingId);

            _meetingFormModelBinder.FormModelToMeeting(formModel, meeting);

            DataContext.Current.Meetings.Update(meeting);
        }

        public void DeleteAll()
        {
            DataContext.Current.Meetings.Clear();
        }

        public void Delete(int meetingId)
        {
            var meeting = DataContext.Current.Meetings.First(x => x.Id == meetingId);
            meeting.DeletedAt = DateTime.Now;
            DataContext.Current.Meetings.Update(meeting);
        }

        private void SetID(Meeting meeting)
        {
            if (DataContext.Current.Meetings.Any()) {
                meeting.Id = DataContext.Current.Meetings.Max(x => x.Id) + 1;
            }
            else {
                meeting.Id = 1;
            }
        }
    }
}