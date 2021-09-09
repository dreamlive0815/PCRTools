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
                                    ScriptOps.PARSE_PVEC2F, "0.00,0.38", ScriptOps.MOVE_TO_BX,
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
                                    ScriptOps.PARSE_PVEC2F, "0.49,0.41", ScriptOps.MOVE_TO_AX,
                                    ScriptOps.DO_CLICK,
                                },
                            },
                        }
                    },

                },
            };
            return script;
        }
    }
}
