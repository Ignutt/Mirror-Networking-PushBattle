﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public abstract class PlayerState
    {
        public Player player;
        
        public readonly Action onEnterState = () => {}; 
        public readonly Action onFinishState = () => {};

        public PlayerState(Player player)
        {
            this.player = player;
        }

        public virtual void Initialize()
        {
            
        }
        
        public virtual void EnterState()
        {
            onEnterState.Invoke();
        }

        public virtual void FinishState()
        {
            onFinishState.Invoke();
        }

        public abstract void Process();
    }
}