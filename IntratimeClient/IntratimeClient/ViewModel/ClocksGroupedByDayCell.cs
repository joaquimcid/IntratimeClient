using System;
using System.Collections.Generic;
using System.Linq;
using IntratimeClient.Services.IntratimeClientAPI.Dto;

namespace IntratimeClient.ViewModel
{
    public class ClocksGroupedByDayCell
    {
        public string WorkingHours
        {
            get
            {
                var entrada = ValueOrNewList(UserAction.Entrada).Min(x => x.Time);
                var pausa = ValueOrNewList(UserAction.Pause).Min(x => x.Time);
                var volver = ValueOrNewList(UserAction.Volver).Min(x => x.Time);
                var salida = ValueOrNewList(UserAction.Salida).Min(x => x.Time);

                if (entrada == default)
                    return string.Empty;
                
                if (salida == default)
                    return string.Empty;

                TimeSpan workingTime = new TimeSpan();
                if (pausa == default || volver == default)
                {
                    workingTime = salida.TimeOfDay.Subtract(entrada.TimeOfDay);
                }
                else
                {
                    
                    workingTime = salida.TimeOfDay.Subtract(volver.TimeOfDay)
                        .Add(pausa.TimeOfDay.Subtract(entrada.TimeOfDay));
                }

                return $"{workingTime.ToString("c")}";
            }
        }


        public bool ClocksOfDayIsValid() =>
            ClocksEntradaCount > 0 && ClocksSalidaCount > 0 && ClocksPausaCount > 0 && ClocksVolverCount > 0;

        public bool ClocksOfDayIsNotValid() => !ClocksOfDayIsValid();

        public DateTime Day { get; set; }
        public Dictionary<UserAction, List<ClockRecordCell>> ClocksOfDay { get; private set; }

        public string ClocksEntradaTimesList => ListOfTimesToString(UserAction.Entrada); 
        public string ClocksPausaTimesList => ListOfTimesToString(UserAction.Pause);
        public string ClocksVolverTimesList => ListOfTimesToString(UserAction.Volver);
        public string ClocksSalidaTimesList => ListOfTimesToString(UserAction.Salida);

        //public List<ClockRecordCell> ClocksEntrada => ValueOrNewList(UserAction.Entrada);
        //public List<ClockRecordCell> ClocksPausa => ValueOrNewList(UserAction.Pause);
        //public List<ClockRecordCell> ClocksVolver => ValueOrNewList(UserAction.Volver);
        //public List<ClockRecordCell> ClocksSalida => ValueOrNewList(UserAction.Salida);


        private int ClocksEntradaCount => ValueOrNewList(UserAction.Entrada).Count;
        private int ClocksPausaCount => ValueOrNewList(UserAction.Pause).Count;
        private int ClocksVolverCount => ValueOrNewList(UserAction.Volver).Count;
        private int ClocksSalidaCount => ValueOrNewList(UserAction.Salida).Count;

        private List<ClockRecordCell> ValueOrNewList(UserAction userAction)
        {
            if (!ClocksOfDay.ContainsKey(userAction)) return new List<ClockRecordCell>();
            if (ClocksOfDay.ContainsKey(userAction) && ClocksOfDay[userAction] == null) return new List<ClockRecordCell>();

            return ClocksOfDay[userAction];
        }

        private string ListOfTimesToString(UserAction userAction)
        {
            var listOfClockRecords = ValueOrNewList(userAction);
            var foo = listOfClockRecords.Select(c => c.Time.ToString("HH:mm:ss"));

            var result = string.Join(System.Environment.NewLine, foo);

            return result;
        }

        public ClocksGroupedByDayCell()
        {
            Day = new DateTime();
            ClocksOfDay = new Dictionary<UserAction, List<ClockRecordCell>>();
        }

        public void AddClockRecord(ClockRecordCell clockRecordCell)
        {
            if (ClocksOfDay == null)
                ClocksOfDay = new Dictionary<UserAction, List<ClockRecordCell>>();

            if(!ClocksOfDay.ContainsKey(clockRecordCell.UserAction))
                ClocksOfDay[clockRecordCell.UserAction] = new List<ClockRecordCell>();
            
            ClocksOfDay[clockRecordCell.UserAction].Add(clockRecordCell);
        }
    }
}