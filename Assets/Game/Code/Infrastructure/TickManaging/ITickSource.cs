﻿using Game.Scripts.Infrastructure.TickManaging;

namespace Code.Infrastructure.UpdateRunner
{
    public interface ITickSource
    {
        void AddListener(ITickListener listener);
        void RemoveListener(ITickListener listener);
    }
}