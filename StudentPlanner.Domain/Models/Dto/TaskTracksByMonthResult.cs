using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.Domain.Models.Dto;

public class TaskTracksByMonthResult
{
    public List<string> Monthes { get; set; }
    public List<double> SpentMonthHours { get; set; }

    public List<string> ProjectTaskNames { get; set; }
    public List<double> ProjectTaskTime { get; set; }
}