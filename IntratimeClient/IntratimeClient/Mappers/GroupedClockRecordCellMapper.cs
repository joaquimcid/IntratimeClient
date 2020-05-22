using System;
using System.Collections.Generic;
using System.Linq;
using IntratimeClient.Services.IntratimeClientAPI.Response.Query;
using IntratimeClient.ViewModel;

namespace IntratimeX.Mappers
{
    public class GroupedClockRecordCellMapper : BaseMapper<ClocksGroupedByDayCell, ClockQueryResponse>
    {
        private readonly ClockRowMapper _rowmMapper;

        public GroupedClockRecordCellMapper(ClockRowMapper clockRowmMapper)
        {
            _rowmMapper = clockRowmMapper;
        }

        public override ClockQueryResponse Map(ClocksGroupedByDayCell dto)
        {
            throw new NotImplementedException();
        }

        public override ClocksGroupedByDayCell Map(ClockQueryResponse entity)
        {
            var clockQueryGrouped = _rowmMapper.Map(entity);
            var result = new ClocksGroupedByDayCell();
            result.Day = new DateTime(entity.InoutDate.Year, entity.InoutDate.Month, entity.InoutDate.Day); 
            
            result.AddClockRecord(clockQueryGrouped);

            return result;
        }

        public override IEnumerable<ClocksGroupedByDayCell> Map(IEnumerable<ClockQueryResponse> list)
        {
            var result = new List<ClocksGroupedByDayCell>();

            foreach (var clockClockQueryResponse in list)
            {
                var day = clockClockQueryResponse.InoutDate;
                var recordType = clockClockQueryResponse.InoutType;


                var clockRecordCell = _rowmMapper.Map(clockClockQueryResponse);
                
                var groupedItemsByDay = new ClocksGroupedByDayCell();

                if (result.Any(x => x.Day.Date.Equals(clockRecordCell.Time.Date)))
                {
                   var foo = result.Where(x => x.Day.Date.Equals(clockRecordCell.Time.Date));
                   groupedItemsByDay = foo.First();
                   groupedItemsByDay.AddClockRecord(clockRecordCell);
                }
                else
                {
                    groupedItemsByDay.Day = clockClockQueryResponse.InoutDate.Date;
                    groupedItemsByDay.AddClockRecord(clockRecordCell);
                    result.Add(groupedItemsByDay);
                }
            }

            return result;
        }
    }
}