﻿using System;
using System.Text;

namespace Ritsukage.QQ.Commands
{
    [CommandGroup]
    public static class Holiday
    {
        [Command("最近节日", "holiday")]
        public static async void Normal(SoraMessage e)
        {
            try
            {
                var h = Library.Roll.Model.Holiday.Recent();
                StringBuilder sb = new();
                sb.AppendLine("[" + DateTime.Today.ToString("yyyy-MM-dd") + "]");
                sb.Append(h[0].ToString());
                for (var i = 1; i < h.Length; i++)
                    sb.AppendLine().Append(h[i].ToString());
                await e.Reply(sb.ToString());
            }
            catch
            {
                await e.Reply("数据获取失败，请稍后再试");
            }
        }
    }
}