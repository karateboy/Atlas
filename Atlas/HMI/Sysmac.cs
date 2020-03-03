using OMRON.Compolet.SYSMAC;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HMI
{
    static class Sysmac
    {
        private static SysmacCJ sysmac = new SysmacCJ();
        public static void Init()
        {
            Debug.WriteLine(message: $"FinsAddr {HmiConfig.Instance.FinsAddr}");
            sysmac.NetworkAddress = 0;
            sysmac.NodeAddress = 240;
            sysmac.UnitAddress = 0;
            sysmac.ReceiveTimeLimit = 5000;
            sysmac.Active = true;
        }

        public static async Task<ValueTuple<bool, float>> ReadFloatAsync(SysmacCSBase.MemoryTypes memoryType, System.Int64 offset)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return (true, sysmac.ReadMemoryDwordSingle(memoryType, offset));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"{ex.Message}");
                    return (false, 0f);
                }
            });
        }
        public static async Task<ValueTuple<bool, float[]>> ReadFloatAsync(SysmacCSBase.MemoryTypes memoryType, System.Int64 offset, System.Int64 count)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var result = sysmac.ReadMemoryDwordSingle(memoryType, offset, count);
                    return (true, result);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"{ex.Message}");
                    return (false, Array.Empty<float>());
                }
            });
        }

        public static async Task<ValueTuple<bool, int>> ReadIntAsync(SysmacCSBase.MemoryTypes memoryType,
            System.Int64 offset, SysmacPlc.DataTypes dataType = SysmacPlc.DataTypes.BIN)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return (true, sysmac.ReadMemoryWordInteger(memoryType, offset, dataType));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"{ex.Message}");
                    return (false, 0);
                }
            });
        }
        public static async Task<ValueTuple<bool, int[]>> ReadIntAsync(SysmacCSBase.MemoryTypes memoryType,
            System.Int64 offset, System.Int64 count, SysmacPlc.DataTypes dataType = SysmacPlc.DataTypes.BIN)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var result = sysmac.ReadMemoryWordInteger(memoryType, offset, count, dataType);
                    return (true, result);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"{ex.Message}");
                    return (false, Array.Empty<int>());
                }
            });
        }

        public static async Task<ValueTuple<bool, bool>> ReadBitAsync(SysmacCSBase.MemoryTypes memoryType, System.Int64 offset, System.Int16 bitOffset)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return (true, sysmac.ReadMemoryBit((long)memoryType, offset, bitOffset));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"{ex.Message}");
                    return (false, false);
                }
            });
        }

        public static async Task<ValueTuple<bool, bool[]>> ReadBitAsync(SysmacCSBase.MemoryTypes memoryType, System.Int64 offset, System.Int16 bitOffset, System.Int64 count)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return (true, sysmac.ReadMemoryBit((long)memoryType, offset, bitOffset, count));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"{ex.Message}");
                    return (false, Array.Empty<bool>());
                }
            });
        }

        public static async Task<bool> WriteBitAsync(SysmacCSBase.MemoryTypes memoryType, System.Int64 offset, System.Int16 bitOffset, bool data)
        {
            return await Task.Run(() =>
            {
                try
                {
                    sysmac.WriteMemoryBit((long)memoryType, offset, bitOffset, data);
                    return true;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    return false;
                }
            });
        }

        public static async Task<bool> WriteIntAsync(SysmacCSBase.MemoryTypes memoryType, System.Int64 offset, int writeData, SysmacPlc.DataTypes dataType = SysmacPlc.DataTypes.BIN)
        {
            return await Task.Run(() =>
            {
                try
                {
                    sysmac.WriteMemoryWordInteger(memoryType, offset, writeData, dataType);
                    return true;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    return false;
                }
            });
        }
    }
}
