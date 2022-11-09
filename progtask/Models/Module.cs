using System;
using System.ComponentModel.DataAnnotations;

public class Module
{
	[Key, MaxLength(50), MinLength(3)]
	public string ModuleCode { get; set; } = null!;
    [MaxLength(50), MinLength(3)]
    public string ModuleName { get; set; } = null!;
    public int Credits { get; set; }
    public int ClassHoursPerWeek { get; set; }
    public int WeeksInSemester { get; set; }
    public DateTime SemesterStartDate { get; set; }
    public double RecommendedHoursToStudy { get; set; }
    public int HoursStudiedToday { get; set; }

    public string Username { get; set; } = null!;

    public override string? ToString()
    {
        return ModuleCode;
    }
}
