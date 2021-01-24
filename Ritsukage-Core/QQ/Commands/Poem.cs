﻿using System;

namespace Ritsukage.QQ.Commands
{
    [CommandGroup]
    public static class Poem
    {
        [Command("飞花令"), NeedCoins(2)]
        public static async void FHL(SoraMessage e, string _char)
        {
            if (_char.Length != 1)
            {
                await e.Reply("参数错误，请检查后重试");
                return;
            }
            var result = await Tools.Poem.Search(_char);
            if (result.Count <= 0)
            {
                await e.Reply("没有搜索到任何结果，请检查后重试");
                return;
            }
            if (result.Count <= 5)
                await e.Reply($"带有「{_char}」字的诗句有：" + Environment.NewLine + string.Join(Environment.NewLine, result));
            else
            {
                var s = new string[5];
                var rand = new Tools.Rand();
                for (var i = 0; i < 5; i++)
                {
                    var id = rand.Int(1, result.Count) - 1;
                    s[i] = result[id];
                    result.RemoveAt(id);
                }
                await e.Reply($"带有「{_char}」字的诗句有(随机选取5句)：" + Environment.NewLine + string.Join(Environment.NewLine, s));
            }
            await e.RemoveCoins(2);
        }
    }
}