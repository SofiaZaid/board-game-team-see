namespace TicTacToe.Mail
{
    interface IMailService
    {
        void SendEmail(string email, string nickName);
    }
}
