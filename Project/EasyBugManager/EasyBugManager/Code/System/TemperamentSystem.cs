/* By: 絮大王（xudawang@vip.163.com）
   Time：2019年11月29日09:23:29*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 性格的系统（用于管理所有的性格）
    /// </summary>
    public class TemperamentSystem
    {
        /* 属性：TemperamentDatas(所有的性格数据)
                 
           方法：GetTemperament(获取性格)(根据Id获取性格的数据)
                 RandomReply(随机[回复])(当Bear说话后，Bug需要回复一句话)
                 RandomCreate(随机[创建])(当创建1个Bug时，需要随机1个Bear说的话，和1个Bug说的话) */



        #region [公开属性]
        /// <summary>
        /// 所有的性格数据
        /// </summary>
        public List<TemperamentData> TemperamentDatas { get; set; }

        #endregion


        #region [构造方法]

        public TemperamentSystem()
        {
            //创建[性格数据]（中文）
            CreateTemperamentData(LanguageType.Chinese);
        }

        #endregion



        #region [公开方法 - 获取]
        /// <summary>
        /// 获取性格
        /// (根据Id获取性格的数据)
        /// </summary>
        /// <param name="_id">性格的编号</param>
        /// <returns>性格的数据</returns>
        public TemperamentData GetTemperament(int _id)
        {
            for (int i = 0; i < TemperamentDatas.Count; i++)
            {
                if (TemperamentDatas[i].Id == _id)
                {
                    return TemperamentDatas[i];
                }
            }

            return null;
        }

        /// <summary>
        /// 随机1个性格
        /// </summary>
        /// <returns></returns>
        public TemperamentData RandomTemperament()
        {
            //随机1个性格
            int _index = RandomTool.Random(0, TemperamentDatas.Count);

            //返回值
            return TemperamentDatas[_index];
        }





        /// <summary>
        /// 获取回复的文字
        /// (根据性格Id和ReplyId，来获取回复的数据)
        /// </summary>
        /// <param name="_temperamentId">性格的编号</param>
        /// <param name="_replyId">回复的编号</param>
        /// <returns>回复的文字</returns>
        public string GetReplyString(int _temperamentId,int _replyId)
        {
            //获取数据
            TemperamentData _temperamentData = GetTemperament(_temperamentId);

            //返回回复的文字
            if (_replyId >= 0 && _replyId<_temperamentData.BugStringInReply.Count)
            {
                return _temperamentData.BugStringInReply[_replyId];
            }

            return "";
        }

        /// <summary>
        /// 获取创建的文字
        /// (根据性格Id和CreateId，来获取创建的数据)
        /// </summary>
        /// <param name="_temperamentId">性格的编号</param>
        /// <param name="_createId">创建的编号</param>
        /// <returns>创建的文字</returns>
        public string GetCreateString(int _temperamentId, int _createId)
        {
            //获取数据
            TemperamentData _temperamentData = GetTemperament(_temperamentId);

            //返回回复的文字
            if (_createId >= 0 && _createId < _temperamentData.BugStringInCreate.Count)
            {
                return _temperamentData.BugStringInCreate[_createId];
            }

            return "";
        }
        #endregion

        #region [公开方法 - 随机]
        /// <summary>
        /// 随机[回复]
        /// (当Bear说话后，Bug需要回复一句话)
        /// </summary>
        /// <param name="_id">性格数据的编号</param>
        /// <returns>随机出来的话的编号</returns>
        public int RandomReply(int _id)
        {
            //首先，获取性格数据
            TemperamentData _temperamentData = GetTemperament(_id);

            //然后，随机1个Bug的回复
            int _randomIndex = RandomTool.Random(0, _temperamentData.BugStringInReply.Count);

            //返回值
            return _randomIndex;
        }

        /// <summary>
        /// 随机[创建]
        /// (当创建1个Bug时，需要随机1个Bug说的话)
        /// </summary>
        /// <param name="_id">性格数据的编号</param>
        /// <param name="_bug">Bug说的话</param>
        public void RandomCreate(out int _id,out string _bug) 
        {
            //随机1个性格
            TemperamentData _temperamentData = RandomTemperament();
            _id = _temperamentData.Id;

            //然后，随机1个Bug说的话
            int _bugIndex = RandomTool.Random(0, _temperamentData.BugStringInCreate.Count);
            _bug = _temperamentData.BugStringInCreate[_bugIndex];
        }

        #endregion



        #region [私有方法 - 数据]
        /// <summary>
        /// 创建[性格数据]
        /// </summary>
        /// <param name="_languageType">是中文的性格？还是英文的性格？</param>
        public void CreateTemperamentData(LanguageType _languageType)
        {
            switch (_languageType)
            {
                case LanguageType.Chinese:
                    CreateTemperamentData_CN();
                    break;
                case LanguageType.English:
                    CreateTemperamentData_EN();
                    break;
            }
        }




        /// <summary>
        /// 创建[性格数据]（中文）
        /// </summary>
        private void CreateTemperamentData_CN()
        {
            TemperamentDatas = new List<TemperamentData>();

            //第1个性格
            {
                TemperamentData _temperament1 = new TemperamentData();
                _temperament1.Id = 0;

                //[创建时]Bug说的话
                _temperament1.BugStringInCreate = new List<string>()
                {
                    "Hello，我是一个你刚发现的Bug。\n你有任何想说的话，都可以跟我说哦~",
                };

                //[回复时]Bug说的话
                _temperament1.BugStringInReply = new List<string>() { 
                    "我知道啦！", 
                    "不用担心，所有问题都会慢慢解决哒！",
                    "你会解决这个Bug哒！因为你有明亮的眼睛，和聪明的头脑~",
                    "一杯茶，一包烟，一个Bug查一天~",
                    "查Bug很耗费精力哒，记得多休息哦~",
                    "哇！你又来跟我聊天啦！超开心~",
                    "为什么这个Bug出现了呢？为什么这个Bug又不见了呢？",
                    "一款游戏一包烟，一台电脑一下午。\n一盒泡面一壶水，一顿能管一整天",
                    "敲一行代码，留两个Bug",
                    "假如生活欺骗了你，不要悲伤不要心急，Bug会一直陪伴着你",
                    "世界上最远的距离，是我在if里，你在else里，\n虽然经常一起出现，但却永不结伴执行。",
                    "如果累了，就睡一觉叭，也许起来之后，所有的一切都会更顺利~",
                    "你可以多和我说说话，说不定你的思路就越来越清晰啦！",
                    "累了的话，就好好休息呀，工作的目的是为了更好的生活，不是吗？",
                    "没事没事，别担心，你随时可以停下来休息下的，不用那么着急。",
                    "我一直在这里陪你。",
                    "风里雨里，Bug一直等你。",
                    "稳住！是不是快下班啦？",
                    "好累呀，我想喝一杯温暖的咖啡，或者冰冰的饮料~",
                    "如果我有人工智能的技术就好了，那样的话，我就可以真的和你聊天，而不是在这里自言自语……",
                    "别嫌我笨呀，我会一直陪你，想与你一起成长",
                    "晚上想吃什么呢？",
                    "Bug可以慢慢解决，饭不能不吃，记得准时吃饭哦~",
                    "我去睡一会，你也休息下吧？",
                    "（吃早饭中……）",
                    "（吃午饭中……）",
                    "（吃晚饭中……）",
                    "（喝下午茶中……）",
                    "（喝咖啡中……）",
                    "（喝牛奶中……）",
                    "（听音乐中……）",
                    "（写日记中……）",
                    "（洗澡中……）",
                    "我是一个小小Bug记录员~",
                    "最优秀的Bug从来不会寻求关注~",
                    "人生和写代码不一样，人生辛苦多了",
                    "有些Bug很容易改，有些Bug不容易发现，可不经意间你会遇到一个彩虹般绚丽的Bug，从此以后其他Bug，不过是浮云",
                    "Bug总是这样，不能每个都解决。但我们还要热情地开发下去",
                    "生活就像一段代码，结果往往出乎意料",
                    "你写代码，结局要么有Bug，要么没Bug，没有其他选择",
                    "也许一段完美的代码，只能让我们不断追求，而却无法真正拥有",
                    "也许，Bug需要的是深深的理解与接受",
                    "抱抱Bug，温暖Bug",
                    "Bug有什么办法，它也很无奈呀=A=",
                    "你想聊聊发生了什么吗？",
                    "我也不知道怎么帮助你，但我随时都在",
                    "没关系，想说的时候随时跟我说，我都在~",
                    "我想成为一名优秀的小天使，在你烦恼的时候，随时安慰你~",
                    "多加断点~",
                    "心情不好吗？我的肚肚给你揉~",
                    "Bug改不了没有关系的，无论如何我都会陪着你~",
                    "给你一个安慰的抱抱~",
                    "不写代码，就没有Bug",
                    "然后呢？",
                    "还有这种事？",
                    "听段音乐舒缓下心情吧？",
                    "我不知道怎么跟你说，但是我相信你总有一天会看到彩虹的~",
                    "我不知道说什么才好，因为我只是一只小Bug呀！",
                    "上次那个问题你都解决的很好，这次的问题肯定也难不到你哒！",
                    "放松，我们可以慢慢再想想",
                    "代码如果出错了可以重来、可以修复。希望人生也能如此~",
                    "我有很多温暖的话，如果有一句能够让你更开心一点，那我就能超级满足了呢",
                    "你每天都这么努力，真是太厉害啦！",
                    "多和我聊聊天吧~",
                    "加油~加油~加油~",
                    "该怎么办才好呢？",
                    "怎么办呢？",
                    "站起来放松下吧",
                    "喝点水吧",
                    "说实话，Bug最害怕的，就是像你这样优秀的程序员",
                    "虽然这些回复是随机产生，但每一句话想传达的，都是满满的温暖呀",
                };

                TemperamentDatas.Add(_temperament1);
            }
        }


        /// <summary>
        /// 创建[性格数据]（英文）
        /// </summary>
        private void CreateTemperamentData_EN()
        {
            TemperamentDatas = new List<TemperamentData>();

            //第1个性格
            {
                TemperamentData _temperament1 = new TemperamentData();
                _temperament1.Id = 0;

                //[创建时]Bug说的话
                _temperament1.BugStringInCreate = new List<string>()
                {
                    "Hello, I'm a Bug you just found.\nYou can tell me anything you want to say."
                };

                //[回复时]Bug说的话
                _temperament1.BugStringInReply = new List<string>()
                {
                    "I've got it!",
                    "Don't worry! Everything's gonna be fine!",
                    "With your sharp eyes and brilliant mind, this bug is a piece of cake!",
                    "Debugging takes a long long time. \nHow about some tea?",
                    "Debugging takes energy. Don't forget to relax!",
                    "Wow! I'm so happy to chat with you again!",
                    "The bug appears! It's gone now!",
                    "Sitting in front of a computer, a programmer's way;\nDebugging inside all codes, a programmer's day.",
                    "Enter your code, leave your bug.",
                    "I will always be there, just for you.",
                    "We're like parallels.\nWe accompany with each other but will never converge.",
                    "If you're tired, get some sleep. \nEverything will go smoothly when you wake up.",
                    "Please chat with me! You're close to Eureka!",
                    "Got tired? Have a relax! You may only go round one time. Live it well!",
                    "That's alright. Don't push yourself that hard. Take a break.",
                    "I will always be with you.",
                    "Bug is always there for you.",
                    "Behold! Ain't it closing time?",
                    "I'm so tired! How about a warm coffee or ice coke?",
                    "If only I had AI technology, I could really talk to you instead of talking to myself here...",
                    "Don't call me stupid. \nI will always accompany you and grow up with you.",
                    "Got any idea for dinner?",
                    "Debugging is a long period task, while your meal is not. \nHave your meals on time!",
                    "I'm gonna have some rest. How about you?",
                    "(Having breakfast...)",
                    "(Having lunch...)",
                    "(Having supper...)",
                    "(Having afternoon tea...)",
                    "(Having coffee...)",
                    "(Having milk...)",
                    "(Listening to music...)",
                    "(Keeping diary...)",
                    "(Bathing...)",
                    "I'm a recorder for bugs.",
                    "Best bugs are always the hardest to find.",
                    "Debugging is tough, while life is tougher.",
                    "Some bugs are easy to be found, while some are not. \nRest awhile, maybe you'll crash into a pot of rainbow!",
                    "Maybe not each bug would be eliminate, the enthusiasm for debugging will always be there.",
                    "Life was like a piece of codes, you never know what you're gonna get.",
                    "To bug, or not to bug. There's no other options.",
                    "A piece of perfect code is like a magic dragon which needs our continuous pursuit.",
                    "Perhaps what bugs need is your deep understanding and acceptance.",
                    "Hug your bug, warm your bug.",
                    "Bug is also helpless.",
                    "Got something to share with me?",
                    "I've got no idea. But I'll be here with you.",
                    "It's okay. \nChat with me whenever you need me.",
                    " I will be your guardian angel.",
                    "More breakpoints please.",
                    "It's okay if you can't debug it. \nI'm here with you.",
                    "A free hug for you!",
                    "Bug No code, no bug.",
                    "And then?",
                    "How come!",
                    "Want some music to relax yourself?",
                    "I don't know, but someday you'll have your moments.",
                    "I don't have a clue. I'm only a little bug.",
                    "You've done perfectly last time. \nI believe in you for this one.",
                    "Relax. Take your time to think about it.",
                    "When a piece of code got wrong, it can start again. \nI wish life could be the same.",
                    "I have a lot of warm words, if there is a can make you happier, then I can be super satisfied.",
                    "You're so awesome since you work so hard everyday!",
                    "I want to listen to more !",
                    "Cheer up !",
                    "What's your plan for this?",
                    "What do we do?",
                    "Take a break.",
                    "Have something to drink!",
                    "What a bug buster you are!",
                    "Although these responses are randomly generated, each sentence is intended to convey warmth to you.",
                };

                TemperamentDatas.Add(_temperament1);
            }
        }

        #endregion

    }
}
