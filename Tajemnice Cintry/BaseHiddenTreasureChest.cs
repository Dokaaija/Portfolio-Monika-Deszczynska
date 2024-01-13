using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Items.Containers
{
    public class BaseHiddenTreasureChest : LockableContainer
    {
        private HiddenTreasureLevel m_TreasureLevel;
        private short m_MaxSpawnTime = 15;
        private short m_MinSpawnTime = 15;
        private int m_DetectLevel = 100;
        private HiddenTreasureResetTimer m_ResetTimer;
        public BaseHiddenTreasureChest(int itemID)
            : this(itemID, HiddenTreasureLevel.Level2)
        {
        }

        public BaseHiddenTreasureChest(int itemID, HiddenTreasureLevel level)
            : base(itemID)
        {
            this.m_TreasureLevel = level;
            this.Locked = false;
            this.Movable = false;
            this.Visible = false;

            //this.SetLockLevel();
            this.GenerateHiddenTreasure();
        }

        public BaseHiddenTreasureChest(Serial serial)
            : base(serial)
        {
        }

        public enum HiddenTreasureLevel
        {
            Level1,
            Level2,
            Level3,
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public HiddenTreasureLevel Level
        {
            get
            {
                return this.m_TreasureLevel;
            }
            set
            {
                this.m_TreasureLevel = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public short MaxSpawnTime
        {
            get
            {
                return this.m_MaxSpawnTime;
            }
            set
            {
                this.m_MaxSpawnTime = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public short MinSpawnTime
        {
            get
            {
                return this.m_MinSpawnTime;
            }
            set
            {
                this.m_MinSpawnTime = value;
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public override bool Locked
        {
            get
            {
                return base.Locked;
            }
            set
            {
                if (base.Locked != value)
                {
                    base.Locked = value;

                    if (!value)
                        this.StartResetTimer();
                }
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int DetectLevel
        {
            get
            {
                return this.m_DetectLevel;
            }
            set
            {
                this.m_DetectLevel = value;
            }
        }
        public override bool IsDecoContainer
        {
            get
            {
                return false;
            }
        }
        public override string DefaultName
        {
            get
            {
                if (this.Locked)
                    return "Zamknieta skrzynia";

                return "Zgubiony Pakunek";
            }
        }
        public override void OnDoubleClick(Mobile from)
        {
            if (!Locked && TrapType != TrapType.None)
            {
                Timer.DelayCall(TimeSpan.FromSeconds(0.6), delegate { this.Delete(); });
            }

            base.OnDoubleClick(from);
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write((byte)this.m_TreasureLevel);
            writer.Write(this.m_MinSpawnTime);
            writer.Write(this.m_MaxSpawnTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            this.m_TreasureLevel = (HiddenTreasureLevel)reader.ReadByte();
            this.m_MinSpawnTime = reader.ReadShort();
            this.m_MaxSpawnTime = reader.ReadShort();

            if (!this.Locked)
                this.StartResetTimer();
        }

        public void ClearContents()
        {
            for (int i = this.Items.Count - 1; i >= 0; --i)
            {
                if (i < this.Items.Count)
                    this.Items[i].Delete();
            }
        }

        public void Reset()
        {
            /* For XML spawner purpose - we delete chest instead of regenerating it
            if (this.m_ResetTimer != null)
            {
                if (this.m_ResetTimer.Running)
                    this.m_ResetTimer.Stop();
            }

            this.Locked = true;
            this.ClearContents();
            this.GenerateTreasure();
            */
            this.Delete();
        }

        /*protected virtual void SetLockLevel()
        {
            switch (this.m_TreasureLevel)
            {
                case HiddenTreasureLevel.Level1:
                    this.RequiredSkill = this.LockLevel = 10;
                    break;
                case HiddenTreasureLevel.Level2:
                    this.RequiredSkill = this.LockLevel = 20;
                    break;
                case HiddenTreasureLevel.Level3:
                    this.RequiredSkill = this.LockLevel = 40;
                    break;
                case HiddenTreasureLevel.Level4:
                    this.RequiredSkill = this.LockLevel = 60;
                    break;
                case HiddenTreasureLevel.Level5:
                    this.RequiredSkill = this.LockLevel = 80;
                    break;
                case HiddenTreasureLevel.Level6:
                    this.RequiredSkill = this.LockLevel = 90;
                    break;
            }
        }*/

        protected virtual void GenerateHiddenTreasure()
        {
            int MinGold = 1;
            int MaxGold = 2;

            switch (this.m_TreasureLevel)
            {
                case HiddenTreasureLevel.Level1:


                    MinGold = 20;
                    MaxGold = 150;
                    if (Utility.RandomDouble() > .5)
                        DropItem(new CzerwonyPyl(Utility.Random(5, 20)));
                    switch (Utility.Random(3))
                    {
                        case 0: DropItem(new CzerwonyPyl(Utility.Random(5, 10))); break;
                        case 1: DropItem(new JedwabneNici(Utility.Random(1, 5))); break;
                        case 2: DropItem(new BrokatoweNici(Utility.Random(1, 3))); break;
                    }
                    switch (Utility.Random(7))
                    {
                        case 0: DropItem(new SilneLekarstwa(Utility.Random(2, 5))); break;
                        case 1: DropItem(new Bursztyn(Utility.Random(1, 2))); break;
                        case 2: DropItem(new SamorodekZlota(Utility.Random(1, 3))); break;
                        case 3: DropItem(new SrebrnyNaszyjnik()); break;
                        case 4: DropItem(new Cukier(Utility.Random(3, 10))); break;
                        case 5: DropItem(new Diament(Utility.Random(1, 2))); break;
                        case 6: DropItem(new ZaszyfrowanySzkic()); break;
                    }
                    switch (Utility.Random(5))
                    {
                        case 0: DropItem(new WywarzRumianku()); break;
                        case 1: DropItem(new SyropCzosnkowy()); break;
                        case 2: DropItem(new NapojOrzezwiajacy()); break;
                        case 3: DropItem(new SrebrnaBransoleta()); break;
                        case 4: DropItem(new Cieciwa(Utility.Random(1, 2))); break;
                        case 5: DropItem(new Sol(Utility.Random(1, 10))); break;
                    }
                    break;
                case HiddenTreasureLevel.Level2:

                    MinGold = 80;
                    MaxGold = 225;
                    if (Utility.RandomDouble() > .5)
                        DropItem(new CzerwonyPyl(Utility.Random(5, 40)));
                    switch (Utility.Random(3))
                    {
                        case 0: DropItem(new Sol(Utility.Random(3, 13))); break;
                        case 1: DropItem(new Cukier(Utility.Random(5, 13))); break;
                        case 2: DropItem(new JedwabneNici(Utility.Random(1, 5))); break;
                    }
                    switch (Utility.Random(7))
                    {
                        case 0: DropItem(new Onyks(Utility.Random(1, 3))); break;
                        case 1: DropItem(new Bursztyn(Utility.Random(1, 3))); break;
                        case 2: DropItem(new Bursztyn(Utility.Random(1, 3))); break;
                        case 3: DropItem(new Perla(Utility.Random(1, 3))); break;
                        case 4: DropItem(new Cytryn(Utility.Random(1, 3))); break;
                        case 5: DropItem(new Ametyst(Utility.Random(1, 2))); break;
                        case 6: DropItem(new Topaz(Utility.Random(1, 2))); break;
                    }
                    switch (Utility.Random(7))
                    {
                        case 0: DropItem(new BrokatoweNici(Utility.Random(1, 4))); break;
                        case 1: DropItem(new Szafir()); break;
                        case 3: DropItem(new GrubaCieciwa(Utility.Random(1, 2))); break;
                        case 4: DropItem(new Jelec(Utility.Random(1, 3))); break;
                        case 5: DropItem(new Futro(Utility.Random(3, 5))); break;
                        case 6: DropItem(new Lekarstwa(Utility.Random(5, 30))); break;
                    }
                    switch (Utility.Random(7))
                    {
                        case 0: DropItem(new PrzybornikSkryby()); break;
                        case 1: DropItem(new Batyst(Utility.Random(2, 4))); break;
                        case 2: DropItem(new ZlotyNaszyjnikSzafir()); break;
                        case 3: DropItem(new SrebrnyNaszyjnikRubin()); break;
                        case 4: DropItem(new Diament(Utility.Random(1, 2))); break;
                        case 5: DropItem(new NiewyraznaRycina()); break;
                        case 6: DropItem(new ZaszyfrowanySzkic()); break;
                    }
                    break;
                case HiddenTreasureLevel.Level3:

                    MinGold = 120;
                    MaxGold = 350;
                    if (Utility.RandomDouble() > .5)
                        DropItem(new CzerwonyPyl(Utility.Random(10, 50)));
                    switch (Utility.Random(5))
                    {
                        case 0: DropItem(new Sol(Utility.Random(5, 20))); break;
                        case 1: DropItem(new Cukier(Utility.Random(5, 20))); break;
                        case 2: DropItem(new JedwabneNici(Utility.Random(2, 6))); break;
                        case 3: DropItem(new SproszkowaneSrebro()); break;
                        case 4: DropItem(new FragmentDwimerytu()); break;
                    }
                    switch (Utility.Random(7))
                    {
                        case 0: DropItem(new PodplomykzRyba(Utility.Random(1, 3))); break;
                        case 1: DropItem(new PodplomykzOwocami(Utility.Random(1, 3))); break;
                        case 2: DropItem(new PodplomykzSerem(Utility.Random(1, 3))); break;
                        case 3: DropItem(new BulkaMiodowa(Utility.Random(1, 4))); break;
                        case 4: DropItem(new BulkaCzosnkowa(Utility.Random(1, 4))); break;
                        case 5: DropItem(new BialaBulka(Utility.Random(1, 4))); break;
                        case 6: DropItem(new PieczonaGolonkaWieprzowa(Utility.Random(1, 2))); break;
                    }
                    if (Utility.RandomDouble() > .5)
                        switch (Utility.Random(4))
                        {
                            case 0: DropItem(new Turmalin()); break;
                            case 1: DropItem(new Rubin()); break;
                            case 3: DropItem(new ZaszyfrowanySzkic()); break;
                            case 4: DropItem(new NiewyraznaRycina()); break;
                        }
                    if (Utility.RandomDouble() > .5)
                        switch (Utility.Random(16))
                        {
                            case 0: DropItem(new Cieciwa(Utility.Random(2, 4))); break;
                            case 1: DropItem(new GrubaCieciwa(Utility.Random(2, 3))); break;
                            case 2: DropItem(new StalowaPlytka(Utility.Random(10, 35))); break;
                            case 3: DropItem(new SamorodekZlota(Utility.Random(2, 5))); break;
                            case 4: DropItem(new SilneLekarstwa(Utility.Random(15, 20))); break;
                            case 5: DropItem(new Sznurek(Utility.Random(2, 8))); break;
                            case 6: DropItem(new Kilof()); break;
                            case 7: DropItem(new MlotekKowalski()); break;
                            case 8: DropItem(new NarzedziaKrawieckie()); break;
                            case 9: DropItem(new NarzedziaCiesielskie()); break;
                            case 10: DropItem(new PrzybornikSkryby()); break;
                            case 11: DropItem(new ZestawLuczarski()); break;
                            case 12: DropItem(new ZaszyfrowanySzkic()); break;
                            case 13: DropItem(new Jedwab(Utility.Random(1, 2))); break;
                            case 14: DropItem(new Batyst(Utility.Random(2, 6))); break;
                            case 15: DropItem(new Futro(Utility.Random(2, 6))); break;
                        }
                    if (Utility.RandomDouble() > .5)
                        switch (Utility.Random(9))
                        {
                            case 0: DropItem(new JedwabneNici(Utility.Random(1, 3))); break;
                            case 1: DropItem(new BrokatoweNici(Utility.Random(1, 3))); break;
                            case 2: DropItem(new SamorodekZlota()); break;
                            case 3: DropItem(new FragmentDwimerytu()); break;
                            case 4: DropItem(new Cukier(Utility.Random(1, 2))); break;
                            case 5: DropItem(new SproszkowaneSrebro()); break;
                            case 6: DropItem(new SrebrnaZastawa()); break;
                            case 7: DropItem(new Sznurek(Utility.Random(1, 10))); break;
                            case 8: DropItem(new Drut(Utility.Random(3, 8))); break;
                        }
                    switch (Utility.Random(14))
                    {
                        case 0: DropItem(new MiedzianyNaszyjnik()); break;
                        case 1: DropItem(new ZelaznyNaszyjnik()); break;
                        case 2: DropItem(new SrebrnyNaszyjnik()); break;
                        case 3: DropItem(new Korale()); break;
                        case 4: DropItem(new MiedzianaBransoleta()); break;
                        case 5: DropItem(new ZelaznaBransoleta()); break;
                        case 6: DropItem(new SrebrnaBransoleta()); break;
                        case 7: DropItem(new MiedzianySygnet()); break;
                        case 8: DropItem(new ZelaznySygnet()); break;
                        case 9: DropItem(new MiedzianeKolczyki()); break;
                        case 10: DropItem(new SrebrnySygnet()); break;
                        case 11: DropItem(new MiedzianyPierscien()); break;
                        case 12: DropItem(new ZelaznyPierscien()); break;
                        case 13: DropItem(new SrebrnyPierscien()); break;
                    }
                    break;

            }
            this.DropItem(new Gold(MinGold, MaxGold));
        }

        private void StartResetTimer()
        {
            if (this.m_ResetTimer == null)
                this.m_ResetTimer = new HiddenTreasureResetTimer(this);
            else
                this.m_ResetTimer.Delay = TimeSpan.FromMinutes(Utility.Random(this.m_MinSpawnTime, this.m_MaxSpawnTime));

            this.m_ResetTimer.Start();
        }

        private class HiddenTreasureResetTimer : Timer
        {
            private readonly BaseHiddenTreasureChest m_Chest;
            public HiddenTreasureResetTimer(BaseHiddenTreasureChest chest)
                : base(TimeSpan.FromMinutes(Utility.Random(chest.MinSpawnTime, chest.MaxSpawnTime)))
            {
                this.m_Chest = chest;
                this.Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                this.m_Chest.Reset();
            }
        }
        ;
    }
}