﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 出窑工位采集服务.ModBus
{
    public interface ILog
    {
        void Write(string log);
    }
}
