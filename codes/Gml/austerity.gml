if scr_passive_skill_is_open(o_pass_skill_asceticism, o_player)
    scr_modifier_change(o_player, o_b_fresh, (200 * sleepHours), 2000)
else
    scr_modifier_change(o_player, o_b_fresh, (150 * sleepHours), 1500)