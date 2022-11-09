using System;

public class Module
{
	[Key, MaxLength(50), MinLength(3)]
	public string ModuleCode { get; set; } = null!;
    [MaxLength(50), MinLength(3)]
    public string ModuleName { get; set; } = null!;
    public int Credits { get; set; } = null!;
    public int ClassHoursPerWeek { get; set; } = null!;
    public int WeeksInSemester { get; set; } = null!;
    public DateTime SemesterStartDate { get; set; } = null!;
    public double RecommendedHoursToStudy { get; set; } = null!;
    public int HoursStudiedToday { get; set; } = null!;
}
