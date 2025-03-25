﻿using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class RequestRegisterUser
    {
        public UserDto User { get; set; }
        public List<VehicleDto>? VehicleList { get; set; }
        public string Password { get; set; }

    }
}
