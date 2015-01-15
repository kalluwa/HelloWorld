using System;
using System.Collections.Generic;

namespace ScriptIn14Days.ASTrees
{
	public interface ASTree
	{
		/// <summary>
		/// get i-th child
		/// </summary>
		/// <param name='i'>
		/// </param>
		ASTree child(int i);
		
		/// <summary>
		/// Numbers the children.
		/// </summary>
		/// <returns>
		/// The children.
		/// </returns>
		int numChildren();
		
		/// <summary>
		/// Children this instance.
		/// </summary>
		List<ASTree> children();
		
		/// <summary>
		/// Location of this instance[line number of token].
		/// </summary>
		string location();

        /// <summary>
        /// get tree value
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        object eval(ScriptIn14Days.Environments.Environment env);
	}
}

