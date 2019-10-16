using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dota2HeroStats.Services.JSONModels
{
#pragma warning disable IDE1006 // Naming Styles
    public class AbiltiyDraftGameJSON
    {
        public long match_id { get; set; }
        public int barracks_status_dire { get; set; }
        public int barracks_status_radiant { get; set; }
        public object chat { get; set; }
        public int cluster { get; set; }
        public object cosmetics { get; set; }
        public int dire_score { get; set; }
        public object draft_timings { get; set; }
        public int duration { get; set; }
        public int engine { get; set; }
        public int first_blood_time { get; set; }
        public int game_mode { get; set; }
        public int human_players { get; set; }
        public int leagueid { get; set; }
        public int lobby_type { get; set; }
        public long match_seq_num { get; set; }
        public int negative_votes { get; set; }
        public object objectives { get; set; }
        public object picks_bans { get; set; }
        public int positive_votes { get; set; }
        public object radiant_gold_adv { get; set; }
        public int radiant_score { get; set; }
        public bool radiant_win { get; set; }
        public object radiant_xp_adv { get; set; }
        public int? skill { get; set; }
        public int start_time { get; set; }
        public object teamfights { get; set; }
        public int tower_status_dire { get; set; }
        public int tower_status_radiant { get; set; }
        public object version { get; set; }
        public Player[] players { get; set; }
        public int patch { get; set; }
        public int region { get; set; }
    }

    public class Player
    {
        public long match_id { get; set; }
        public int player_slot { get; set; }
        public int[] ability_upgrades_arr { get; set; }
        public object ability_uses { get; set; }
        public int? account_id { get; set; }
        public object actions { get; set; }
        public object additional_units { get; set; }
        public int assists { get; set; }
        public int backpack_0 { get; set; }
        public int backpack_1 { get; set; }
        public int backpack_2 { get; set; }
        public object buyback_log { get; set; }
        public object camps_stacked { get; set; }
        public object creeps_stacked { get; set; }
        public object damage { get; set; }
        public object damage_inflictor { get; set; }
        public object damage_inflictor_received { get; set; }
        public object damage_taken { get; set; }
        public int deaths { get; set; }
        public int? denies { get; set; }
        public object dn_t { get; set; }
        public object firstblood_claimed { get; set; }
        public int? gold { get; set; }
        public int? gold_per_min { get; set; }
        public object gold_reasons { get; set; }
        public int? gold_spent { get; set; }
        public object gold_t { get; set; }
        public int? hero_damage { get; set; }
        public int? hero_healing { get; set; }
        public object hero_hits { get; set; }
        public int hero_id { get; set; }
        public int item_0 { get; set; }
        public int item_1 { get; set; }
        public int item_2 { get; set; }
        public int item_3 { get; set; }
        public int item_4 { get; set; }
        public int item_5 { get; set; }
        public object item_uses { get; set; }
        public object kill_streaks { get; set; }
        public object killed { get; set; }
        public object killed_by { get; set; }
        public int kills { get; set; }
        public object kills_log { get; set; }
        public object lane_pos { get; set; }
        public int? last_hits { get; set; }
        public int leaver_status { get; set; }
        public int level { get; set; }
        public object lh_t { get; set; }
        public object life_state { get; set; }
        public object max_hero_hit { get; set; }
        public object multi_kills { get; set; }
        public object obs { get; set; }
        public object obs_left_log { get; set; }
        public object obs_log { get; set; }
        public object obs_placed { get; set; }
        public object party_id { get; set; }
        public object party_size { get; set; }
        public object performance_others { get; set; }
        public object permanent_buffs { get; set; }
        public object pings { get; set; }
        public object pred_vict { get; set; }
        public object purchase { get; set; }
        public object purchase_log { get; set; }
        public object randomed { get; set; }
        public object repicked { get; set; }
        public object roshans_killed { get; set; }
        public object rune_pickups { get; set; }
        public object runes { get; set; }
        public object runes_log { get; set; }
        public object sen { get; set; }
        public object sen_left_log { get; set; }
        public object sen_log { get; set; }
        public object sen_placed { get; set; }
        public object stuns { get; set; }
        public object teamfight_participation { get; set; }
        public object times { get; set; }
        public int? tower_damage { get; set; }
        public object towers_killed { get; set; }
        public int? xp_per_min { get; set; }
        public object xp_reasons { get; set; }
        public object xp_t { get; set; }
        public bool radiant_win { get; set; }
        public int start_time { get; set; }
        public int duration { get; set; }
        public int cluster { get; set; }
        public int lobby_type { get; set; }
        public int game_mode { get; set; }
        public int patch { get; set; }
        public int region { get; set; }
        public bool isRadiant { get; set; }
        public int win { get; set; }
        public int lose { get; set; }
        public int? total_gold { get; set; }
        public int? total_xp { get; set; }
        public float kills_per_min { get; set; }
        public int kda { get; set; }
        public int abandons { get; set; }
        public int? rank_tier { get; set; }
        public object[] cosmetics { get; set; }
        public Benchmarks benchmarks { get; set; }
        public string personaname { get; set; }
        public object name { get; set; }
        public object last_login { get; set; }
    }

    public class Benchmarks
    {
        public Gold_Per_Min gold_per_min { get; set; }
        public Xp_Per_Min xp_per_min { get; set; }
        public Kills_Per_Min kills_per_min { get; set; }
        public Last_Hits_Per_Min last_hits_per_min { get; set; }
        public Hero_Damage_Per_Min hero_damage_per_min { get; set; }
        public Hero_Healing_Per_Min hero_healing_per_min { get; set; }
        public Tower_Damage tower_damage { get; set; }
    }

    public class Gold_Per_Min
    {
        public int raw { get; set; }
        public float? pct { get; set; }
    }

    public class Xp_Per_Min
    {
        public int raw { get; set; }
        public float? pct { get; set; }
    }

    public class Kills_Per_Min
    {
        public float raw { get; set; }
        public float? pct { get; set; }
    }

    public class Last_Hits_Per_Min
    {
        public float raw { get; set; }
        public float? pct { get; set; }
    }

    public class Hero_Damage_Per_Min
    {
        public float raw { get; set; }
        public float? pct { get; set; }
    }

    public class Hero_Healing_Per_Min
    {
        public float raw { get; set; }
        public float? pct { get; set; }
    }

    public class Tower_Damage
    {
        public int raw { get; set; }
        public float? pct { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}