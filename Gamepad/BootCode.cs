using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamepad
{
    public static class BootCode
    {
        public static int Run(string bootCode)
        {
            var code = bootCode.Split(Environment.NewLine, StringSplitOptions.None);
            var instructionHash = new HashSet<int>();
            var acc = 0;
            var index = 0;
            var oldIndex = 0;
            while(true)
            {
                if(!instructionHash.Add(index))
                {
                    break;
                }
                if(index > code.Count()-1)
                {
                    break;
                }
                var instruction = code[index];
                oldIndex = index;
                if(instruction.StartsWith("acc"))
                {
                    acc += int.Parse(instruction.Replace("+", string.Empty).Split(" ", StringSplitOptions.None).LastOrDefault());
                    index++;
                }
                else if (instruction.StartsWith("jmp"))
                {
                    index += int.Parse(instruction.Replace("+", string.Empty).Split(" ", StringSplitOptions.None).LastOrDefault());
                }
                else if(instruction.StartsWith("nop"))
                {
                    index++;
                }
                else
                {
                    index++;
                }
            }
            return acc;
        }
    }
}
