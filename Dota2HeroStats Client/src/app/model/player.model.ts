import { Hero } from './hero.model';
import { Ability } from './ability.model';

export class Player {
    constructor(public PlayerSlot?: number, public AccountId?: number, public Item0?: number, public Item1?: number,
        public Item2?: number, public Item3?: number, public Item4?: number, public Item5?: number, public Backpack0?: number,
        public Backpack1?: number, public Backpack2?: number, public HeroLevel?: number, public Kills?: number,
        public Deaths?: number, public Assists?: number, public LastHits?: number, public Denies?: number,
        public GoldPerMin?: number, public XpPerMin?: number, public HeroDamage?: number, public HeroHealing?: number,
        public TowerDamage?: number, public Hero?: Hero, public Abilities?: Ability[]
    ) { }
}
