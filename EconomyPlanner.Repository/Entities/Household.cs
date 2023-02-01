﻿using EconomyPlanner.Repository.Entities.Bases;

namespace EconomyPlanner.Repository.Entities;

public class Household : EntityBase
{
    public string Guid { get; set; }
    public ICollection<EconomyPlan> EconomyPlans { get; set; }
    public Household(string name, string guid) : base(name)
    {
        Guid = guid;
        EconomyPlans = new HashSet<EconomyPlan>();
    }

    public static Household Create(string name)
    {
        return new Household(name, System.Guid.NewGuid().ToString());
    }
}