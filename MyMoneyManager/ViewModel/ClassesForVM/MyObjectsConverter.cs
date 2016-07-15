using MyMoneyManager.Model;

namespace MyMoneyManager.ViewModel.ClassesForVM
{
    public class MyObjectsConverter
    {
        static public void ConvertVOtoBO(IViewElement VO, out IMoneyElement newBO)
        {
            VO.ConvertToBO(out newBO);
        }

        static public void ConvertBOtoVO(IMoneyElement BO, out IViewElement newVO)
        {
            BO.ConvertToVO(out newVO);
        }
    }
}
