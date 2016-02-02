using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    interface IAttackable
    {

        void DealDamage(int dmg);
        bool IsDead();
    }
}
