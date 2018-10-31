using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrongProject
{
  public class workObjectManage
  {	/// <summary>
	/// RightStation
	/// </summary>
	/// <param name="RightStation"></param>
	public RightStation  tag_RightStation;
	/// <summary>
	/// LeftStation
	/// </summary>
	/// <param name="LeftStation"></param>
	public LeftStation  tag_LeftStation;
	/// <summary>
	/// totalRest
	/// </summary>
	/// <param name="totalRest"></param>
	public totalRest  tag_totalRest;

	
      /// <summary>
	
      
    /// <summary>
     /// tag_workObject
    /// </summary>
    public List<object> tag_workObject;

    public  workObjectManage(Work _Work, List<object> workObject)
    {

        //AddWorkObjectManage
	tag_RightStation= new RightStation(_Work);

	workObject.Add(  tag_RightStation);

	tag_LeftStation= new LeftStation(_Work);

	workObject.Add(  tag_LeftStation);

	tag_totalRest= new totalRest(_Work);

	workObject.Add(  tag_totalRest);



	/*   tag_RightStation= new RightStation(_Work);

	   workObject.Add(  tag_RightStation);

	    tag_LeftStation= new LeftStation(_Work);

	    workObject.Add(  tag_LeftStation);
*/
      

    }
 
  }
}
