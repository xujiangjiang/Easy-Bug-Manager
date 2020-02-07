/* By: 絮大王（sukiup@163.com）
   Time：2019年10月10日04:53:50*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace EasyBugManager
{
    /// <summary>
    /// 音效的系统（用于播放音效）
    /// </summary>
    public class AudioSystem
    {
        private SoundPlayer buttonDownSoundPlayer;//[普通按钮按下]的音效
        private SoundPlayer buttonUpSoundPlayer;//[普通按钮抬起]的音效

        #region 构造方法
        public AudioSystem()
        {
            /* 构造SoundPlayer的对象 */
            //[普通按钮按下]+[普通按钮抬起]的音效
            buttonDownSoundPlayer = new SoundPlayer(Properties.Resources.ButtonDown);
            buttonUpSoundPlayer = new SoundPlayer(Properties.Resources.ButtonUp);


            /* 加载音频（提前加载音频）*/
            buttonDownSoundPlayer.Load();
            buttonUpSoundPlayer.Load();
        }
        #endregion

        #region 公开方法-[播放+停止 音效]
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="_audioType">音效类型</param>
        public void PlayAudio(AudioType _audioType)
        {
            //如果不播放声音
            if (AppManager.Datas.SettingsData.Sound == false)return;

            //播放声音
            switch (_audioType)
            {
                case AudioType.ButtonDown:
                    buttonDownSoundPlayer.Play();
                    break;
                case AudioType.ButtonUp:
                    buttonUpSoundPlayer.Play();
                    break;
            }

        }
        #endregion
    }



    /// <summary>
    /// 音效的类型
    /// </summary>
    public enum AudioType : byte
    {
        None,
        ButtonDown,//按钮按下
        ButtonUp//按钮抬起
    }
}
