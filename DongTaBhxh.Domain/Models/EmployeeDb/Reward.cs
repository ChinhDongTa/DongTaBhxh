﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace DongTaBhxh.Domain.Models.EmployeeDb;

public partial class Reward
{
    public int RewardId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public string? Type { get; set; }

    public string? Classify { get; set; }

    public virtual ICollection<QuarterDepartmentRank> QuarterDepartmentRanks { get; set; } = new List<QuarterDepartmentRank>();

    public virtual ICollection<QuarterEmployeeRank> QuarterEmployeeRanks { get; set; } = new List<QuarterEmployeeRank>();
}