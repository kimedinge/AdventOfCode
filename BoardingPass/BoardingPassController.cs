using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardingPass
{
    public static class BoardingPassController
    {
        public static int FindMissingSeat(string boardingPasses)
        {
            var seats = new Dictionary<int, List<int>>();
            foreach(var boardingPass in boardingPasses.Split("\r\n"))
            {
                var row = GetRow(boardingPass);
                var seat = GetSeat(boardingPass);
                if(seats.ContainsKey(row))
                {
                    seats[row].Add(seat);
                }
                else
                {
                    seats.Add(row, new List<int> { seat });
                }
            }
            var rowMissingASeat = seats.Where(r => r.Value.Count() == 7).Select(r => r).FirstOrDefault();
            var pRow = rowMissingASeat.Key;
            var possibleSeats = Enumerable.Range(0, 8);
            var pSeat = 0;
            foreach(var possibleSeat in possibleSeats)
            {
                if(!rowMissingASeat.Value.Contains(possibleSeat))
                {
                    pSeat = possibleSeat;
                }
            }
            var seatId = pRow * 8 + pSeat;
            return seatId;
        }
        public static int GetSeatId(string boardingPasses)
        {
            var boardingPass = boardingPasses.Split("\r\n").FirstOrDefault();
            var row = GetRow(boardingPass);
            var seat = GetSeat(boardingPass);
            var seatId = row * 8 + seat;
            return seatId;
        }

        public static int GetMaxSeatId(string boardingPasses)
        {
            var maxSeatId = 0;
            foreach(var boardingPass in boardingPasses.Split("\r\n"))
            {
                var seatId = GetSeatId(boardingPass);
                if(seatId > maxSeatId)
                {
                    maxSeatId = seatId;
                }
            }
            return maxSeatId;
        }

        private static int GetSeat(string boardingPass)
        {
            var seatString = boardingPass.Substring(6);
            var seats = Enumerable.Range(0, 8).ToList();
            foreach(var side in seatString)
            {
                if(side.Equals('L'))
                {
                    seats = seats.Take(seats.Count / 2).ToList();
                }
                if(side.Equals('R'))
                {
                    seats = seats.TakeLast(seats.Count / 2).ToList();
                }
            }
            return seats.FirstOrDefault();
        }

        private static int GetRow(string boardingPass)
        {
            var rowString = boardingPass.Substring(0, 7);
            var rows = Enumerable.Range(0,128).ToList();
            foreach (var half in rowString)
            {
                if (half.Equals('F'))
                {
                    rows = rows.Take(rows.Count / 2).ToList();
                }
                if(half.Equals('B'))
                {
                    rows = rows.TakeLast(rows.Count / 2).ToList();
                }
            }
            return rows.FirstOrDefault();
        }
    }
}
