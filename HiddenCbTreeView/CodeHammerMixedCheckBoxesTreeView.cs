/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CodeHammerHiddenCheckBoxTreeNodeNameSpace
{
    /// <summary>
    /// this class CodeHammerMixedCheckBoxesTreeView
    /// </summary>
    public class CodeHammerMixedCheckBoxesTreeView : TreeView
    {
        /// <summary>
        /// Specifies or receives attributes of a node
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TV_ITEM
        {
            public int Mask;
            public IntPtr ItemHandle;
            public int State;
            public int StateMask;
            public IntPtr TextPtr;
            public int TextMax;
            public int Image;
            public int SelectedImage;
            public int Children;
            public IntPtr LParam;
        }

        /// <summary>
        /// The tvi f_ state
        /// </summary>
        public const int TVIF_STATE = 0x8;

        /// <summary>
        /// The tvi s_ stateimagemask
        /// </summary>
        public const int TVIS_STATEIMAGEMASK = 0xF000;

        /// <summary>
        /// The tv m_ setitema
        /// </summary>
        public const int TVM_SETITEMA = 0x110d;

        /// <summary>
        /// The tv m_ setitem
        /// </summary>
        public const int TVM_SETITEM = 0x110d;

        /// <summary>
        /// The tv m_ setitemw
        /// </summary>
        public const int TVM_SETITEMW = 0x113f;

        /// <summary>
        /// The tv m_ getitem
        /// </summary>
        public const int TVM_GETITEM = 0x110C;

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref TV_ITEM lParam);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // trap TVM_SETITEM message
            if (m.Msg == TVM_SETITEM || m.Msg == TVM_SETITEMA || m.Msg == TVM_SETITEMW)
                // check if CheckBoxes are turned on
                if (CheckBoxes)
                {
                    // get information about the node
                    TV_ITEM tv_item = (TV_ITEM)m.GetLParam(typeof(TV_ITEM));
                    HideCheckBox(tv_item);
                }
        }

        /// <summary>
        /// Hides the CheckBox.
        /// </summary>
        /// <param name="tv_item">The tv_item.</param>
        protected void HideCheckBox(TV_ITEM tv_item)
        {
            if (tv_item.ItemHandle != IntPtr.Zero)
            {
                // get TreeNode-object, that corresponds to TV_ITEM-object
                TreeNode currentTN = TreeNode.FromHandle(this, tv_item.ItemHandle);

                CodeHammerHiddenCheckBoxTreeNode hiddenCheckBoxTreeNode = currentTN as CodeHammerHiddenCheckBoxTreeNode;
                // check if it's HiddenCheckBoxTreeNode and if its checkbox already has been hidden

                if (hiddenCheckBoxTreeNode != null)
                {
                    HandleRef treeHandleRef = new HandleRef(this, Handle);

                    // check if checkbox already has been hidden
                    TV_ITEM currentTvItem = new TV_ITEM();
                    currentTvItem.ItemHandle = tv_item.ItemHandle;
                    currentTvItem.StateMask = TVIS_STATEIMAGEMASK;
                    currentTvItem.State = 0;

                    IntPtr res = SendMessage(treeHandleRef, TVM_GETITEM, 0, ref currentTvItem);
                    bool needToHide = res.ToInt32() > 0 && currentTvItem.State != 0;

                    if (needToHide)
                    {
                        // specify attributes to update
                        TV_ITEM updatedTvItem = new TV_ITEM();
                        updatedTvItem.ItemHandle = tv_item.ItemHandle;
                        updatedTvItem.Mask = TVIF_STATE;
                        updatedTvItem.StateMask = TVIS_STATEIMAGEMASK;
                        updatedTvItem.State = 0;

                        // send TVM_SETITEM message
                        SendMessage(treeHandleRef, TVM_SETITEM, 0, ref updatedTvItem);
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> that contains the event data.</param>
        protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
        {
            base.OnBeforeCheck(e);

            // prevent checking/unchecking of HiddenCheckBoxTreeNode, otherwise, we will have to
            // repeat checkbox hiding
            if (e.Node is CodeHammerHiddenCheckBoxTreeNode)
                e.Cancel = true;
        }
    }
}