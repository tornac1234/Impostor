using Impostor.Api.Events;

namespace Impostor.Plugins.RootPostor.Handlers
{
    public class AnnouncementsListener : IEventListener
    {
        private const int Id = 51;

        /*[EventListener]
        public void OnAnnouncementRequestEvent(IAnnouncementRequestEvent e)
        {
            if (e.Id == Id)
            {
                // Client already has announcement cached, lets just use that
                e.Response.UseCached = true;
            }
            else
            {
                // Client is receiving this announcement for the first time, window will popup 
                e.Response.Announcement = new Announcement(Id, "Tornac's servers news:" +
                    "\n- Custom server is now supported");
            }
        }*/
    }
}
