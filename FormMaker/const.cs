		//public enum PERIODIC_TYPE                   // периодичность формы
		//{
		//	NULL_PERIODIC = -1                      // отсутствие значения (только для программ C#) (соответствует NULL в БД)
		//	, EMPTY_PERIODIC = 0                    // нет или статика
		//	, YEARS = 1                             // годовая
		//	, HALF_YEARS = 2                        // полугодовая
		//	, QUARTERS = 4                          // квартальная
		//	, MONTHS = 12                           // ежемесячная
		//	, HALF_MONTHS = 24                      // полумесячная
		//	, TEN_DAYS = 36                         // ежедекадная
		//	, WEEKS = 52                            // еженедельная
		//	, DAYS = 365                            // ежедневная
		//};
		//public static string GetPeriodicName(PERIODIC_TYPE periodic)
		//{
		//	switch (periodic)
		//	{
		//		case PERIODIC_TYPE.NULL_PERIODIC: return "нет";
		//		case PERIODIC_TYPE.EMPTY_PERIODIC: return "статика";
		//		case PERIODIC_TYPE.YEARS: return "год";
		//		case PERIODIC_TYPE.HALF_YEARS: return "полугодие";
		//		case PERIODIC_TYPE.QUARTERS: return "квартал";
		//		case PERIODIC_TYPE.MONTHS: return "месяц";
		//		case PERIODIC_TYPE.HALF_MONTHS: return "полумесяц";
		//		case PERIODIC_TYPE.TEN_DAYS: return "декада";
		//		case PERIODIC_TYPE.WEEKS: return "неделя";
		//		case PERIODIC_TYPE.DAYS: return "день";
		//		default: break;
		//	}
		//	return null;
		//}
		public enum DATASUMMATION_TYPE  // данные подлежат своду
		{
			SUMMATION_NONE = 0
			, SUMMATION_NO = 1
			, SUMMATION_YES = 2
		}

		public enum DATAMANDATORY_TYPE  // данные обязательны для ввода
		{
			MANDATORY_NONE = 0
			, MANDATORY_NO = 1
			, MANDATORY_YES = 2
		}

		public enum DATACOPYING_TYPE    // данные подлежат копированию
		{
			COPYING_NONE = 0
			, COPYING_NO = 1
			, COPYING_YES = 2
		}
		public enum DATA_TYPE           // типы данных колонок, строк, ячеек
		{
			EMPTY_DATA = 0          // пустышка
			, DOUBLE_DATA = 1           // число с плавающей точкой
			, TEXT_DATA = 2             // строка символов (до 2000 символов)
			, DATE_DATA = 3             // дата
			, DATETIME_DATA = 4         // дата и время
			, INTEGER_DATA = 5          // целое число
			, INTRANGE_DATA = 6         // целое число в диапазоне					(пока не сделано)
			, BOOL_DATA = 7             // буль 2 состояния (да/нет) (1/0)
			, CLOB_DATA = 11            // cLob - большой текст						(пока не сделано)
			, BLOB_DATA = 12            // bLob - большой бинарный объект			(пока не сделано)
			, FORMREF_DATA = 21         // ссылка на вложенную (nested) форму
			, CHECKLIST_DATA = 22       // список check								(пока не сделано)
			, RADIOLIST_DATA = 23       // список radio								(пока не сделано)
			, LISTBOX_DATA = 24         // listBox									(пока не сделано)
			, COMBOBOX_DATA = 25        // comboBox
			, TREE_DATA = 26            // tree										(пока не сделано)
			, LINK_DATA = 31            // ссылка в интернет						(пока не сделано)
		};		