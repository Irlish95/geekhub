﻿using Geekhub.App.Modules.Alerts.Data;
using Geekhub.App.Modules.Meetings.Data;
using Geekhub.App.Modules.Users.Data;
using System.Web.Mvc;

namespace Geekhub.App.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private AlertsService _alertsService = new AlertsService();
        private AlertsRepository _alertsRepository = new AlertsRepository();
        private MeetingsService _meetingsService = new MeetingsService();
        private MeetingsRepository _meetingsRepository = new MeetingsRepository();
        private UsersService _usersService = new UsersService();
        private UsersRepository _usersRepository = new UsersRepository();

        public UsersService UsersService
        {
            get
            {
                return _usersService;
            }
        }

        public UsersRepository UsersRepository
        {
            get
            {
                return _usersRepository;
            }
        }


        public AlertsService AlertsService
        {
            get
            {
                return _alertsService;
            }
        }

        public AlertsRepository AlertsRepository
        {
            get
            {
                return _alertsRepository;
            }
        }

        public MeetingsService MeetingsService
        {
            get
            {
                return _meetingsService;
            }
        }

        public MeetingsRepository MeetingsRepository
        {
            get
            {
                return _meetingsRepository;
            }
        }

        protected void Notice(string message)
        {
            TempData["notice"] = message;
        }

        protected void Warn(string message)
        {
            TempData["warn"] = message;
        }
    }
}