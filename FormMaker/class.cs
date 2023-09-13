using Microsoft.AspNetCore.Mvc.Rendering;

public class Form{
	public int formId {get;set;}
	public int master {get;set;}
	public int periodic {get;set;}
	public int kind {get;set;} //2 для вводных, 10 для аналитических
	public int modeenter {get;set;}
	public int modevertical {get;set;}
	public int hassubject {get;set;}
	public int hasfacility {get;set;}
	public int hasterritory {get;set;}
	public string code {get; set;}
	public string shortName {get; set;}
	public string name {get; set;}
	public string note {get; set;}
	public string department {get; set;}
	public DateTime formdate {get; set;}
	public DateTime since {get; set;}
    static List<TypeOfPeriod> PeriodicForm { get; } = new()
        {
            new TypeOfPeriod(-1,"Отсутствие значения"),
            new TypeOfPeriod(0,"Нет или статика"),
            new TypeOfPeriod(1,"Годовая"),
            new TypeOfPeriod(2,"Полугодовая"),
            new TypeOfPeriod(4,"Квартальная"),
            new TypeOfPeriod(12,"Ежемесячная"),
            new TypeOfPeriod(24,"Полумесячная"),
            new TypeOfPeriod(36,"Ежедекадная"),
            new TypeOfPeriod(52,"Еженедельная"),
            new TypeOfPeriod(365,"Ежедневная"),
        };
    public record class TypeOfPeriod(int Id, string Name);
    public static SelectList ListOfPeriod { get; } = new SelectList(PeriodicForm, "Id", "Name");
}

public class FormColumn{
	public int formId {get; set;}
	public int formcolumn {get; set;}
	public string name {get; set;}
	public string note {get; set;}
	public int type {get; set;}
	public string compute {get; set;}
	public string precompute {get; set;}
	public string outercompute {get; set;}
	public int summation {get; set;}
	public int mandatory {get; set;}
	public int copyng {get; set;}
}

public class FormRow{
	public int formId {get; set;}
	public int formrow {get; set;}
	public string code {get; set;}
	public string name {get; set;}
	public string note {get; set;}
	public int type {get; set;}
	public string unit {get; set;}
	public string balancesign {get; set;}
	public string okp {get; set;}
	public string measure {get; set;}
	public string compute {get; set;}
	public int mandatory {get; set;}
	public int copyng {get; set;}
}

public class FormCell{
	public int formId {get; set;}
	public int formrow {get; set;}
	public int formcolumn {get; set;}
	public int type {get; set;}
	public int summation {get; set;}
	public int mandatory {get; set;}
	public int copyng {get; set;}
}
