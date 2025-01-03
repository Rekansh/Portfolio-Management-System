﻿namespace PortfolioManagement.Entity.Analysis
{
    public class VolumeEntity
    {
        public int ScriptId { get; set; } = 0;
        public string ScriptName { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public double PriceChange { get; set; } = 0;
        public double PercentageChange { get; set; } = 0;
        public double Volume { get; set; } = 0;
        public double WeekVolumeAverage { get; set; } = 0;
        public double MonthVolumeAverage { get; set; } = 0;
        public double WeekVolumePercentage { get; set; } = 0;
        public double MonthVolumePercentage { get; set; } = 0;
        public int NewsCount { get; set; } = 0; //integer 
        
    }
    public class VolumeGridEntity
    {
        public List<VolumeEntity> Volumes { get; set; } = new List<VolumeEntity>();
    }

    public class VolumeParameterEntity
    {
        public DateTime DateTime { get; set; } = new DateTime(0);
    }
}
