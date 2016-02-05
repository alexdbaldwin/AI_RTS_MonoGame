using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_RTS_MonoGame
{
    class DelayedDamage : IComparable<DelayedDamage>
    {
        int dmg;
        float delay;

        public int Dmg { get { return dmg; } }

        public DelayedDamage(int dmg, float delay)
        {
            this.dmg = dmg;
            this.delay = delay;
        }

        public bool DecreaseTime(float time)
        {
            delay -= time;
            if (delay <= 0.0f)
                return true;
            return false;
        }

        public int CompareTo(DelayedDamage other)
        {
            if (other.delay > delay)
                return -1;
            else if (other.delay == delay)
                return 0;
            else
                return 1;
        }
    }
}
