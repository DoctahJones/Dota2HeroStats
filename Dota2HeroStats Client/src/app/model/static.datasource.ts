import { Injectable } from "@angular/core";
import { Hero } from "./hero.model";
import { Ability } from "./ability.model";
import { HeroAbilityStat } from "./heroAbilityStat.model";
import { Player } from './player.model';
import { AbilityDraftMatch } from './abilityDraftMatch.model';
import { Observable, of } from 'rxjs';

@Injectable()
export class StaticDataSource {
    private heroData: Hero[];
    private abilityData: Ability[];
    private heroAbilityStatData: HeroAbilityStat[];
    private playerData: Player[];
    private abilityDraftMatchData: AbilityDraftMatch[];

    constructor() {
        this.heroData = new Array<Hero>(
            new Hero(1, "npc_dota_hero_antimage", "Anti-Mage", "agi", "Melee", 2, ["Carry", "Escape", "Nuker"]),
            new Hero(2, "npc_dota_hero_axe", "Axe", "str", "Melee", 2, ["Initiator", "Durable", "Disabler", "Jungler"]),
            new Hero(3, "npc_dota_hero_bane", "Bane", "int", "Ranged", 4, ["Support", "Disabler", "Nuker", "Durable"]),
            new Hero(4, "npc_dota_hero_bloodseeker", "Bloodseeker", "agi", "Melee", 2, ["Carry", "Disabler", "Jungler", "Nuker", "Initiator"]),
            new Hero(5, "npc_dota_hero_crystal_maiden", "Crystal Maiden", "int", "Ranged", 2, ["Support", "Disabler", "Nuker", "Jungler"]),
            new Hero(6, "npc_dota_hero_drow_ranger", "Drow Ranger", "agi", "Ranged", 2, ["Carry", "Disabler", "Pusher"]),
            new Hero(7, "npc_dota_hero_earthshaker", "Earthshaker", "str", "Melee", 2, ["Support", "Initiator", "Disabler", "Nuker"]),
            new Hero(8, "npc_dota_hero_juggernaut", "Juggernaut", "agi", "Melee", 2, ["Carry", "Pusher", "Escape"]),
            new Hero(9, "npc_dota_hero_mirana", "Mirana", "agi", "Ranged", 2, ["Carry", "Support", "Escape", "Nuker", "Disabler"]),
            new Hero(10, "npc_dota_hero_morphling", "Morphling", "agi", "Ranged", 0, ["Carry", "Escape", "Durable", "Nuker", "Disabler"]),
            new Hero(11, "npc_dota_hero_nevermore", "Shadow Fiend", "agi", "Ranged", 0, ["Carry", "Nuker"]),
            new Hero(12, "npc_dota_hero_phantom_lancer", "Phantom Lancer", "agi", "Melee", 2, ["Carry", "Escape", "Pusher", "Nuker"]),
            new Hero(13, "npc_dota_hero_puck", "Puck", "int", "Ranged", 2, ["Initiator", "Disabler", "Escape", "Nuker"]),
            new Hero(14, "npc_dota_hero_pudge", "Pudge", "str", "Melee", 2, ["Disabler", "Initiator", "Durable", "Nuker"]),
        )
        this.abilityData = new Array<Ability>(
            new Ability(5003, "Mana Break", "anti-mage-mana-break-5003-a6a469cb89e075790ec16420e1874dafc6b459f8ed60e49559b10f6b81cb2719.jpg", false, 1),
            new Ability(5004, "Blink", "anti-mage-blink-5004-fe84e312ce0bb56da08495794d6dae0c5a0b38cd94e7ef4e5a77f7623abe34ff.jpg", false, 1),
            new Ability(5005, "Spell Shield", "anti-mage-spell-shield-5005-45e604ddd0c9a30e41cff593b1ceb34ead4cfcaee175916a7b83afa68dc5765e.jpg", false, 1),
            new Ability(5006, "Mana Void", "anti-mage-mana-void-5006-803fc47e49d7bb11fa8ce5df24b17f3b46500cc3e45b764a451682a196d8b991.jpg", true, 1),

            new Ability(5023, "Fissure", "earthshaker-fissure-5023-3e4e13e831f3affaf57cf594784e22d365354b106f0247948706da15bd01fc7c.jpg", false, 7),
            new Ability(5024, "Enchant Totem", "earthshaker-enchant-totem-5024-fa4a62f9b71d69a443adcb8a2100dd5ad6b839bc59dc2a00b02311f394557435.jpg", false, 7),
            new Ability(5025, "Aftershock", "earthshaker-aftershock-5025-97fb59bbaa7e0b900880b3a5d8897f331f834d4bf4b55501e2dca883abe90a0c.jpg", false, 7),
            new Ability(5026, "Echo Slam", "earthshaker-echo-slam-5026-1a64647c0854d5d245765ed72773cb314262001b678087445c97500d9f287637.jpg", true, 7),

            new Ability(5012, "Enfeeble", "bane-enfeeble-5012-6da100300c2ed9bdad136b495334eb55734251f772855696ac6efe700cab5f55.jpg", false, 3),
            new Ability(5011, "Brain Sap", "bane-brain-sap-5011-135a8e3cf6a8432344909847907b9c86b30616e5fcea1f9db880e07569b550eb.jpg", false, 3),
            new Ability(5014, "Nightmare", "bane-nightmare-5014-6e694f66da3c336f18edada5cbcbf2f32ef05eb114c304f3862427bce89ebcba.jpg", false, 3),
            new Ability(5013, "Fiend's Grip", "bane-fiends-grip-5013-6406b6e8e78bbc6c23b6c372ae56b461194744d61afbcfa2bde85f8ac4eb9324.jpg", true, 3),
        )

        this.heroAbilityStatData = new Array<HeroAbilityStat>(
            new HeroAbilityStat(1, 5003, 27, 6),
            new HeroAbilityStat(1, 5004, 27, 1),
            new HeroAbilityStat(1, 5005, 27, 25),
            new HeroAbilityStat(7, 5006, 15, 12),
            new HeroAbilityStat(1, 5024, 33, 3),
            new HeroAbilityStat(7, 5026, 100, 14),
            new HeroAbilityStat(3, 5012, 17, 12),
            new HeroAbilityStat(3, 5006, 14, 11),
            new HeroAbilityStat(1, 5013, 45, 7),
            new HeroAbilityStat(7, 5011, 17, 8)
        )

        this.playerData = new Array<Player>(
            new Player(0, 218661849, 48, 100, 223, 187, 108, 77, 77, 0, 0,
                22, 10, 10, 10, 126, 0, 460, 467, 16448, 0, 7653, this.heroData[0],
                [this.abilityData[0], this.abilityData[1], this.abilityData[2], this.abilityData[3]]
            ),
            new Player(1, 127343337, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 15, 8, 29, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[1],
                [this.abilityData[4], this.abilityData[5], this.abilityData[6], this.abilityData[7]]
            ),
            new Player(2, 301007482, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 15, 8, 14, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[2],
                [this.abilityData[8], this.abilityData[9], this.abilityData[10], this.abilityData[11]]
            ),
            new Player(3, null, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 11, 8, 2, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[3],
                [this.abilityData[4], this.abilityData[2], this.abilityData[0], this.abilityData[7]]
            ),
            new Player(4, null, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 15, 8, 23, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[4],
                [this.abilityData[9], this.abilityData[4], this.abilityData[6], this.abilityData[3]]
            ),
            new Player(128, null, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 12, 8, 26, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[6],
                [this.abilityData[9], this.abilityData[4], this.abilityData[6], this.abilityData[3]]
            ),
            new Player(129, null, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 15, 8, 29, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[7],
                [this.abilityData[9], this.abilityData[4], this.abilityData[6], this.abilityData[3]]
            ),
            new Player(130, null, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 23, 8, 9, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[8],
                [this.abilityData[9], this.abilityData[4], this.abilityData[6], this.abilityData[3]]
            ),
            new Player(131, null, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 15, 8, 29, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[10],
                [this.abilityData[9], this.abilityData[4], this.abilityData[6], this.abilityData[3]]
            ),
            new Player(132, null, 147, 166, 236, 48, 160, 139, 0, 0, 0,
                25, 7, 7, 11, 296, 45, 686, 687, 52676, 0, 5394, this.heroData[11],
                [this.abilityData[9], this.abilityData[4], this.abilityData[6], this.abilityData[3]]
            )

        )

        this.abilityDraftMatchData = new Array<AbilityDraftMatch>(
            new AbilityDraftMatch(4219578020, 1542207616, 2671, 227, 38, null, 18, 37, 54, true,
                [this.playerData[0], this.playerData[1], this.playerData[2], this.playerData[3], this.playerData[4],
                this.playerData[5], this.playerData[6], this.playerData[7], this.playerData[8], this.playerData[9]]
            ),
            new AbilityDraftMatch(4219578025, 1542207715, 7680, 227, 38, null, 18, 47, 44, false,
                [this.playerData[0], this.playerData[1], this.playerData[2], this.playerData[3], this.playerData[4],
                this.playerData[5], this.playerData[6], this.playerData[7], this.playerData[8], this.playerData[9]]
            ),

        )
    }
    getHeroData(): Observable<Hero[]> {
        return of(this.heroData);
    }

    getAbilityData(): Observable<Ability[]> {
        return of(this.abilityData);
    }

    getHeroAbilityStatData(): Observable<HeroAbilityStat[]> {
        return of(this.heroAbilityStatData);
    }

    getPlayerData(): Player[] {
        return this.playerData;
    }

    getAbilityDraftMatch(id: number): Observable<AbilityDraftMatch> {
        return of(this.abilityDraftMatchData.find(m => m.MatchId == id));
    }

    getAbilityDraftMatchData(): Observable<AbilityDraftMatch[]> {
        return of(this.abilityDraftMatchData);
    }

    updateHero(hero: Hero): Observable<any> {
        return of(true);
    }

    updateAbility(ability: Ability): Observable<any> {
        return of(true);
    }

    getLoginData(): Observable<string> {
        return of("1234567890");
    }

}