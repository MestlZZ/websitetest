using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.ObjectMothers
{
    public static class BoardObjectMother
    {
        public static Board Create(User user, string title = "New board")
        {
            var board = new Board(user, title);

            board.Criterions.Add(new Criterion(board, true));
            board.Criterions.Add(new Criterion(board, false));

            board.Rights.Add(new Right(board, user, RoleName.Admin));

            return board;
        }
    }
}
