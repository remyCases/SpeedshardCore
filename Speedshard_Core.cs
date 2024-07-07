// Copyright (C) 2024 Rémy Cases
// See LICENSE file for extended copyright information.
// This file is part of the Speedshard repository from https://github.com/remyCases/SpeedshardCore.

using ModShardLauncher;
using ModShardLauncher.Mods;

namespace Speedshard_Core;
public class SpeedshardCore : Mod
{
    public override string Author => "zizani";
    public override string Name => "Speedshard - Core";
    public override string Description => "Core functionalities of Speedshard";
    public override string Version => "2.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        // ini globals
        Msl.AddMenu("Core",
            new UIComponent(name:"Accelerated Gamespeed", associatedGlobal:"accelerated_gamespeed", UIComponentType.Slider, (40, 160), 40),
            new UIComponent(name:"Reputation Multiplier", associatedGlobal:"reputation_modifier", UIComponentType.Slider, (1, 10), 1),
            new UIComponent(name:"Water Container Mult", associatedGlobal:"water_container_modifier", UIComponentType.Slider, (1, 10), 1, true),
            new UIComponent(name:"Days Campbeds Despawn", associatedGlobal:"campbed_despawn_days", UIComponentType.Slider, (1, 10), 1, true),
            new UIComponent(name:"Hostile Spawnrate By Lvl", associatedGlobal:"hostile_spawnrate_by_level_modifier", UIComponentType.Slider, (1, 41), 41),
            new UIComponent(name:"Neutral Spawnrate By Lvl", associatedGlobal:"neutral_spawnrate_by_level_modifier", UIComponentType.Slider, (1, 41), 41),
            new UIComponent(name:"Xp by Level Modifier", associatedGlobal:"xp_by_level_modifier", UIComponentType.Slider, (100, 1000), 500, true),
            new UIComponent(name:"Max Level", associatedGlobal:"max_level", UIComponentType.Slider, (20, 40), 30, true),
            new UIComponent(name:"Interval Level Extra SP", associatedGlobal:"interval_level_extra_sp", UIComponentType.Slider, (1, 41), 41, true),
            new UIComponent(name:"Interval Level Extra AP", associatedGlobal:"interval_level_extra_ap", UIComponentType.Slider, (1, 41), 41, true),
            new UIComponent(name:"Extra SP&AP At Creation", associatedGlobal:"extra_sp_ap_at_creation", UIComponentType.CheckBox, 0, true),
            new UIComponent(name:"Quicksave Enable", associatedGlobal:"quicksave_enable", UIComponentType.CheckBox, 0),
            new UIComponent(name:"Campbed Not Destroyed", associatedGlobal:"campbed_not_destroyed", UIComponentType.CheckBox, 0)
        );

        SpeedshardIni();
        MorePointAtStart();
        Reputation();
        SetWaterCharge();
        XpDistribution();
        MoreSPAP();
        MaxLevel();
        Speed();
        QuickSave();
        Campbed();
        SpawnRate();
        Austerity();
    }

    private void SpeedshardIni()
    {
        Msl.LoadGML("gml_GlobalScript_scr_sessionDataInit")
            .MatchFrom("scr_sessionDataInit\n{")
            .InsertBelow(ModFiles, "load_ini.gml")
            .Save();
    }
    private void MorePointAtStart()
    {
        Msl.LoadGML("gml_GlobalScript_scr_characterMapInit")
            .MatchFrom("ds_map_add(global.characterDataMap, \"SP\", 2)") // match the 2 lines below the moment I found OldXP
            .InsertBelow(ModFiles, "more_point_at_start.gml") // replace by the code in that file
            .Save(); // save it back
    }
    private void Reputation()
    {
        Msl.LoadGML("gml_GlobalScript_scr_village_reputation_add")
            .MatchFrom("_reputation_prev + argument0")
            .InsertAbove(ModFiles, "village_reputation_add.gml")
            .Save();
            
        Msl.LoadGML("gml_GlobalScript_scr_reputationLog")
            .MatchFrom(@"ds_map_set(_logDataMap, ""reputation"", argument1)")
            .InsertAbove(ModFiles, "reputationLog.gml")
            .Save();
    }
    public void SetWaterCharge()
    {
        List<string> WaterContainers = new()
        {
            "gml_Object_o_inv_wooden_bucket_water_Create_0",
            "gml_Object_o_inv_hunting_horn_water_Create_0",
            "gml_Object_o_inv_golden_cup_water_Create_0",
            "gml_Object_o_inv_clay_pot_water_Create_0",
            "gml_Object_o_inv_wineskin_Create_0"
        };
        
        foreach(string item in WaterContainers)
            Msl.LoadGML(item)
                .MatchFrom("charge")
                .InsertBelow(ModFiles, "water.gml")
                .Save();
    }
    public void XpDistribution()
    {
        Msl.LoadAssemblyAsString("gml_Object_o_player_Step_0")
            .MatchFromUntil("push.s \"OldXP\"", "popz.v")
            .ReplaceBy(ModFiles, "player_xp_distribution.asm")
            .Save();
    }
    public void MoreSPAP()
    {
        Msl.LoadAssemblyAsString("gml_Object_o_player_Step_0")
            .MatchFromUntil("call.i gml_Script_scr_psychic_accumulation\npopz.v", "popz.v")
            .ReplaceBy(ModFiles, "more_ap_sp_by_level.asm")
            .MatchFromUntil("pop.v.v local._oldXp", "popz.v")
            .ReplaceBy("pop.v.v local._oldXp")
            .Save();
    }
    public static void MaxLevel()
    {
        Msl.LoadAssemblyAsString("gml_Object_o_player_Step_0")
            .MatchFrom("pushi.e 30\ncmp.i.v EQ")
            .ReplaceBy("pushglb.v global.max_level\ncmp.v.v EQ")
            .Save();

        Msl.LoadGML("gml_Object_o_character_panel_mask_Draw_0")
            .MatchFrom("var _maxLevel = (scr_atr(\"LVL\") == 30)")
            .ReplaceBy("var _maxLevel = (scr_atr(\"LVL\") == global.max_level)")
            .Save();
    }
    public void Speed()
    {
        Msl.LoadGML("gml_Object_o_player_KeyPress_115")
            .MatchAll()
            .ReplaceBy(ModFiles, "speed_keypress.gml")
            .Save();

        Msl.LoadGML("gml_Object_o_player_Create_0")
            .MatchAll()
            .InsertAbove("game_set_speed(global.gamespeed, gamespeed_fps)")
            .Save();
    }
    public void QuickSave()
    {
        Msl.LoadGML("gml_Object_o_player_KeyPress_116")
            .MatchAll()
            .ReplaceBy(ModFiles, "quicksave_keypress.gml")
            .Save();
    }
    public void Campbed()
    {
        Msl.LoadGML("gml_Object_c_bed_sleep_crafted_Alarm_0")
            .MatchFrom("if (_days")
            .ReplaceBy("if (_days >= global.campbed_despawn_days)")
            .Save();

        Msl.LoadGML("gml_Object_o_sleepController_Other_12")
            .MatchFromUntil("with (owner)", "}")
            .ReplaceBy(ModFiles, "campbed_used.gml")
            .Save();
    }
    public void SpawnRate()
    {
        Msl.LoadAssemblyAsString("gml_Object_o_biome_generation_controller_Create_0")
            .MatchFromUntil(":[28]", "pop.v.v self.NeutralSlots")
            .ReplaceBy(ModFiles, "spawnrate.asm")
            .Save();
    }
    public void Austerity()
    {
        Msl.LoadGML("gml_Object_o_sleepController_Other_10")
            .MatchFrom("scr_modifier_change")
            .ReplaceBy(ModFiles, "austerity.gml")
            .Save();
    }
}
