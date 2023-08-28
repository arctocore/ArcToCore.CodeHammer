/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

using System.Runtime.Serialization;
using System.Windows.Forms;

namespace CodeHammerHiddenCheckBoxTreeNodeNameSpace
{
    /// <summary>
    /// this class CodeHammerHiddenCheckBoxTreeNode
    /// </summary>
    public class CodeHammerHiddenCheckBoxTreeNode : TreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerHiddenCheckBoxTreeNode"/> class.
        /// </summary>
        public CodeHammerHiddenCheckBoxTreeNode()
        {
        }

        public CodeHammerHiddenCheckBoxTreeNode(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerHiddenCheckBoxTreeNode"/> class.
        /// </summary>
        /// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node.</param>
        /// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects.</param>
        public CodeHammerHiddenCheckBoxTreeNode(string text, TreeNode[] children)
            : base(text, children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerHiddenCheckBoxTreeNode"/> class.
        /// </summary>
        /// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node.</param>
        /// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected.</param>
        /// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected.</param>
        public CodeHammerHiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex)
            : base(text, imageIndex, selectedImageIndex)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerHiddenCheckBoxTreeNode"/> class.
        /// </summary>
        /// <param name="text">The label <see cref="P:System.Windows.Forms.TreeNode.Text" /> of the new tree node.</param>
        /// <param name="imageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is unselected.</param>
        /// <param name="selectedImageIndex">The index value of <see cref="T:System.Drawing.Image" /> to display when the tree node is selected.</param>
        /// <param name="children">An array of child <see cref="T:System.Windows.Forms.TreeNode" /> objects.</param>
        public CodeHammerHiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children)
            : base(text, imageIndex, selectedImageIndex, children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeHammerHiddenCheckBoxTreeNode"/> class.
        /// </summary>
        /// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the data to deserialize the class.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream.</param>
        protected CodeHammerHiddenCheckBoxTreeNode(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}