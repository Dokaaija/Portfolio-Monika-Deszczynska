using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Containers
{
    [FlipableAttribute(0xe43, 0xe42)]
    public class HiddenTreasureBag : BaseHiddenTreasureChest
    {
        [Constructable]
        public HiddenTreasureBag()
            : base(0xE43)
        {
        }

        public HiddenTreasureBag(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0xe41, 0xe40)]
    public class MetalGoldenTreasureChest : BaseTreasureChest
    {
        [Constructable]
        public MetalGoldenTreasureChest()
            : base(0xE41)
        {
        }

        public MetalGoldenTreasureChest(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    [FlipableAttribute(0x9ab, 0xe7c)]
    public class MetalTreasureChest : BaseTreasureChest
    {
        [Constructable]
        public MetalTreasureChest()
            : base(0x9AB)
        {
        }

        public MetalTreasureChest(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class ZgubionaSakwa1 : BaseHiddenTreasureChest
    {
        [Constructable]
        public ZgubionaSakwa1()
            : base(Utility.RandomList(0x0E76), HiddenTreasureLevel.Level1)
        {
            DetectLevel = 50;
            /*if (Utility.RandomDouble() > .6)
            {
                TrapType = TrapType.ExplosionTrap;
                TrapPower = 1;
            }*/
        }
        public ZgubionaSakwa1(Serial serial)
            : base(serial) { }
        public override void Serialize(GenericWriter writer)
        { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader)
        { base.Deserialize(reader); int version = reader.ReadInt(); }


    }

    public class ZgubionaSakwa2 : BaseHiddenTreasureChest
    {
        [Constructable]
        public ZgubionaSakwa2()
            : base(Utility.RandomList(0x0E75, 0x09B2, 0x0E76), HiddenTreasureLevel.Level2)
        {
            DetectLevel = 70;
            /* if (Utility.RandomDouble() > .5)
             {
                 TrapType = TrapType.ExplosionTrap;
                 TrapPower = 25;
             }*/
        }
        public ZgubionaSakwa2(Serial serial)
            : base(serial) { }
        public override void Serialize(GenericWriter writer)
        { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader)
        { base.Deserialize(reader); int version = reader.ReadInt(); }
    }
    public class ZgubionaSakwa3 : BaseHiddenTreasureChest
    {
        [Constructable]
        public ZgubionaSakwa3()
            : base(Utility.RandomList(0x0E75, 0x09B2), HiddenTreasureLevel.Level3)
        {
            DetectLevel = 90;
            /*if (Utility.RandomDouble() > .4)
            {
                TrapType = TrapType.ExplosionTrap;
                TrapPower = 45;
            }*/
        }
        public ZgubionaSakwa3(Serial serial)
            : base(serial) { }
        public override void Serialize(GenericWriter writer)
        { base.Serialize(writer); writer.Write((int)0); }
        public override void Deserialize(GenericReader reader)
        { base.Deserialize(reader); int version = reader.ReadInt(); }
    }

}
