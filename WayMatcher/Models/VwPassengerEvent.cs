﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WayMatcherBL.Models;

public partial class VwPassengerEvent
{
    public int EventId { get; set; }

    public int? EventTypeId { get; set; }

    public int? FreeSeats { get; set; }

    public string Description { get; set; }

    public DateTime? StartTimestamp { get; set; }

    public int? ScheduleId { get; set; }

    public int? StatusId { get; set; }
}