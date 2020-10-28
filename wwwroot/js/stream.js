import http from './http.js';
import { selector, setCountDown, nextMatches } from './manager.js';

http('live/main').asGet().then(data => {
    if(data){     

        if(!data.isLive)
            setCountDown(data.eventDate, data.id);
    }
});
