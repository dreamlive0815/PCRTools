using System;
using System.Collections.Generic;

namespace Core.Script
{
    public class ScriptGenerator
    {

        public static Script GenReliabilityScript()
        {
            var script = new Script()
            {
                Identity = "Reliability",
                Name = "活动关卡信赖度",

                StopWhenException = false,

                Segments =
                {
                    new Segment()
                    {
                        Priority = 100,
                        Conditions =
                        {
                            new Condition() { MatchKey = "download_without_voice", },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                        }
                    },

                    new Segment()
                    {
                        Priority = 70,
                        Conditions =
                        {
                            new Condition() { MatchKey = "reliability_entry_text", },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                        }
                    },

                    new Segment()
                    {
                        Priority = 90,
                        Comment = "if DO match reliability_title AND DO match reliability_new_tag",
                        Conditions =
                        {
                            new Condition() { MatchKey = "reliability_title", },
                            new Condition() { MatchKey = "reliability_new_tag", },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.00,0.10", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.CLICK_TEMPLATE,
                                },
                            },
                        }
                    },
                    new Segment()
                    {
                        Priority = 90,
                        Comment = "if DO match reliability_title AND NOT match reliability_new_tag",
                        Conditions =
                        {
                            new Condition() { MatchKey = "reliability_title", },
                            new Condition() { MatchKey = "reliability_new_tag", OpCodes = new List<string>() { ScriptOps.NOT, } },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.77,0.70", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.PARSE_PVEC2F, "0.77,0.23", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.PARSE_INT, "1200", ScriptOps.MOVE_TO_CX,
                                    ScriptOps.DO_DRAG,
                                },
                            },
                        }

                    },

                    new Segment()
                    {
                        Priority = 80,
                        Comment = "if DO match reliability_present_preview AND DO match reliability_item_new_tag",
                        Conditions =
                        {
                            new Condition() { MatchKey = "reliability_present_preview", },
                            new Condition() { MatchKey = "reliability_item_new_tag", },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.10,0.10", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.CLICK_TEMPLATE,
                                },
                            },
                        }
                    },
                    new Segment()
                    {
                        Priority = 80,
                        Comment = "if DO match reliability_present_preview AND NOT match reliability_item_new_tag, click back button",
                        Conditions =
                        {
                            new Condition() { MatchKey = "reliability_present_preview", },
                            new Condition() { MatchKey = "reliability_item_new_tag", OpCodes = new List<string>() { ScriptOps.NOT, } },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.03,0.05", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.DO_CLICK,
                                },
                            },
                        }
                    },

                    new Segment()
                    {
                        Priority = 0,
                        Comment = "default, click choice 2",
                        Conditions =
                        {
                            new Condition() { OpCodes = new List<string>() { ScriptOps.PARSE_BOOL, "true" } },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.30,0.41", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.DO_CLICK,
                                },
                            },
                        }
                    },

                },
            };
            return script;
        }

        public static Script GenStagelineAutoBattle()
        {
            var script = new Script()
            {
                Identity = "Stageline",
                Name = "关卡自动推图",

                StopWhenException = false,

                Segments =
                {


                    new Segment()
                    {
                        Priority = 120,
                        Comment = "click button_close",
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_close", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 110,
                        Comment = "click button_ok",
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_ok", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 100,
                        Comment = "click button_shop_cancel",
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_shop_cancel", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 95,
                        Comment = "click reliability_win_cancel",
                        Conditions =
                        {
                            new Condition() { MatchKey = "reliability_win_cancel", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 90,
                        Comment = "click stage_next_tag",
                        Conditions =
                        {
                            new Condition() { MatchKey = "stage_normal_on", },
                            new Condition() { MatchKey = "stage_normal_off", OpCodes = new List<string>() { ScriptOps.STACK_OR } },
                            new Condition() { MatchKey = "stage_next_tag", },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.00,0.14", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.CLICK_TEMPLATE,
                                },
                            },
                        }
                    },
                    new Segment()
                    {
                        Priority = 90,
                        Comment = "in stageline scene, but no stage_next_tag and counter < 5, add counter and click left top",
                        Conditions =
                        {
                            new Condition() { MatchKey = "stage_normal_on", },
                            new Condition() { MatchKey = "stage_normal_off", OpCodes = new List<string>() { ScriptOps.STACK_OR } },
                            new Condition() { MatchKey = "stage_next_tag", OpCodes = new List<string>() { ScriptOps.NOT } },
                            new Condition() {
                                OpCodes = new List<string>() {
                                    ScriptOps.PUSH_STRING, "counter_no_stage_new_tag", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.PUSH_STRING, CompareOps.SMALLER, ScriptOps.MOVE_TO_CX,
                                    ScriptOps.PARSE_INT, "5", ScriptOps.MOVE_TO_DX,
                                    ScriptOps.CMP_COUNTER,
                                }
                            },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PUSH_STRING, "counter_no_stage_new_tag", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.PARSE_INT, "1", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.ADD_COUNTER,
                                    ScriptOps.PARSE_PVEC2F, "0.30,0.05", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.DO_CLICK,
                                },
                            },
                        }
                    },
                    new Segment()
                    {
                        Priority = 90,
                        Comment = "in stageline scene, but no stage_next_tag and counter >= 5, do drag and reset counter",
                        Conditions =
                        {
                            new Condition() { MatchKey = "stage_normal_on", },
                            new Condition() { MatchKey = "stage_normal_off", OpCodes = new List<string>() { ScriptOps.STACK_OR } },
                            new Condition() { MatchKey = "stage_next_tag", OpCodes = new List<string>() { ScriptOps.NOT } },
                            new Condition() {
                                OpCodes = new List<string>() {
                                    ScriptOps.PUSH_STRING, "counter_no_stage_new_tag", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.PUSH_STRING, CompareOps.GREATER_OR_EQUAL, ScriptOps.MOVE_TO_CX,
                                    ScriptOps.PARSE_INT, "5", ScriptOps.MOVE_TO_DX,
                                    ScriptOps.CMP_COUNTER,
                                }
                            },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.55,0.5", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.PARSE_PVEC2F, "0.5,0.5", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.PARSE_INT, "1000", ScriptOps.MOVE_TO_CX,
                                    ScriptOps.DO_DRAG,
                                },
                            },
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PUSH_STRING, "counter_no_stage_new_tag", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.PARSE_INT, "0", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.SET_COUNTER,
                                },
                            },
                        }
                    },

                    new Segment()
                    {
                        Priority = 80,
                        Comment = "click enter_battle",
                        Conditions =
                        {
                            new Condition() { MatchKey = "enter_battle", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 70,
                        Comment = "click enter_battle_prepare",
                        Conditions =
                        {
                            new Condition() { MatchKey = "enter_battle_prepare", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 60,
                        Comment = "click battle_next_step",
                        Conditions =
                        {
                            new Condition() { MatchKey = "battle_next_step", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },


                    new Segment()
                    {
                        Priority = 0,
                        Comment = "default, click left top",
                        Conditions =
                        {
                            new Condition() { OpCodes = new List<string>() { ScriptOps.PARSE_BOOL, "true" } },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.30,0.05", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.DO_CLICK,
                                },
                            },
                        }
                    },
                }
            };
            return script;
        }

        public static Script GenReadMainStoryScript()
        {
            var script = new Script()
            {
                Identity = "MainStory",
                Name = "主线剧情",

                StopWhenException = false,

                Segments =
                {

                    new Segment()
                    {
                        Priority = 150,
                        Conditions =
                        {
                            new Condition() { MatchKey = "download_without_voice", },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                        }
                    },
                    new Segment()
                    {
                        Priority = 130,
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_close", },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                        }
                    },

                    new Segment()
                    {
                        Priority = 80,
                        Conditions =
                        {
                            new Condition() { MatchKey = "story_new_tag", },
                        },
                        Actions =
                        {
                            new Action() {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.00,0.10", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.CLICK_TEMPLATE
                                },
                            },
                        }
                    },

                    new Segment()
                    {
                        Priority = 110,
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_skip", },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                        }
                    },
                    new Segment()
                    {
                        Priority = 100,
                        Conditions =
                        {
                            new Condition() { MatchKey = "skip_tag", },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                        }
                    },
                    new Segment()
                    {
                        Priority = 90,
                        Conditions =
                        {
                            new Condition() { MatchKey = "menu_tag", },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                        }
                    },

                    new Segment()
                    {
                        Priority = 50,
                        Comment = "in storylist scene, counter < 3, add counter",
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_back", },
                            new Condition() {
                                OpCodes = new List<string>() {
                                    ScriptOps.PUSH_STRING, "counter_main_story_new_tag", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.PUSH_STRING, CompareOps.SMALLER, ScriptOps.MOVE_TO_CX,
                                    ScriptOps.PARSE_INT, "3", ScriptOps.MOVE_TO_DX,
                                    ScriptOps.CMP_COUNTER,
                                }
                            },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PUSH_STRING, "counter_main_story_new_tag", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.PARSE_INT, "1", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.ADD_COUNTER,
                                },
                            },
                        }
                    },
                    new Segment()
                    {
                        Priority = 50,
                        Comment = "in storylist scene, counter >= 3, click back and reset counter",
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_back", },
                            new Condition() {
                                OpCodes = new List<string>() {
                                    ScriptOps.PUSH_STRING, "counter_main_story_new_tag", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.PUSH_STRING, CompareOps.GREATER_OR_EQUAL, ScriptOps.MOVE_TO_CX,
                                    ScriptOps.PARSE_INT, "3", ScriptOps.MOVE_TO_DX,
                                    ScriptOps.CMP_COUNTER,
                                }
                            },
                        },
                        Actions =
                        {
                            new Action() { OpCodes = { ScriptOps.CLICK_TEMPLATE }, },
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PUSH_STRING, "counter_main_story_new_tag", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.PARSE_INT, "0", ScriptOps.MOVE_TO_BX,
                                    ScriptOps.SET_COUNTER,
                                },
                            },
                        }
                    },

                    new Segment()
                    {
                        Priority = 0,
                        Comment = "default, click center",
                        Conditions =
                        {
                            new Condition() { OpCodes = new List<string>() { ScriptOps.PARSE_BOOL, "true" } },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    ScriptOps.PARSE_PVEC2F, "0.50,0.50", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.DO_CLICK,
                                },
                            },
                        }
                    },
                }
            };
            return script;
        }

        public static Script GenSpecialBattleScript()
        {
            var script = new Script()
            {
                Identity = "BattleOfLegions",
                Name = "军团之战",

                StopWhenException = false,

                Segments =
                {


                    new Segment()
                    {
                        Priority = 120,
                        Comment = "click button_close",
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_close", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 110,
                        Comment = "click button_ok",
                        Conditions =
                        {
                            new Condition() { MatchKey = "button_ok", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 80,
                        Comment = "click enter_battle",
                        Conditions =
                        {
                            new Condition() { MatchKey = "enter_battle", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 70,
                        Comment = "click enter_battle_prepare",
                        Conditions =
                        {
                            new Condition() { MatchKey = "enter_battle_prepare", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },

                    new Segment()
                    {
                        Priority = 60,
                        Comment = "click battle_next_step",
                        Conditions =
                        {
                            new Condition() { MatchKey = "battle_next_step", },
                        },
                        Actions =
                        {
                            new Action() {  OpCodes = { ScriptOps.CLICK_TEMPLATE, } },
                        }
                    },


                    new Segment()
                    {
                        Priority = 0,
                        Comment = "default, click boss",
                        Conditions =
                        {
                            new Condition() { OpCodes = new List<string>() { ScriptOps.PARSE_BOOL, "true" } },
                        },
                        Actions =
                        {
                            new Action()
                            {
                                OpCodes = {
                                    //ScriptOps.PARSE_PVEC2F, "0.8036,0.3323", ScriptOps.MOVE_TO_AX,//卡里莎
                                    //ScriptOps.PARSE_PVEC2F, "0.5755,0.5381", ScriptOps.MOVE_TO_AX,//猪哥
                                    //ScriptOps.PARSE_PVEC2F, "0.3585,0.5899", ScriptOps.MOVE_TO_AX,//美空
                                    ScriptOps.PARSE_PVEC2F, "0.1518,0.3857", ScriptOps.MOVE_TO_AX,//兰法
                                    ScriptOps.DO_CLICK,
                                },
                            },
                        }
                    },
                }
            };
            return script;
        }


        public static Script GenTestScript()
        {
            return GenSpecialBattleScript();
        }
    }
}
