using System;

namespace ecs;

public class Signature
{
    private ulong bits;

    protected Signature Toggle<T>(bool value)
    {
        int index = SignatureManager.GetComponentBit<T>();

        if (value)
            bits |= (1UL << index);
        else
            bits &= ~(1UL << index);
        return this;
    }

    public bool Get(int index) => (bits & (1UL << index)) != 0;

    public bool Matches(Signature other) => (bits & other.bits) == bits;

    public override string ToString() => Convert.ToString((long)bits, 2).PadLeft(64, '0');
}

