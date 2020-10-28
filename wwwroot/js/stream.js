import http from './http.js';
import { selector, setCountDown, nextMatches } from './manager.js';

let myid = selector('matchId');

http('live/main/'+ myid.value).asGet().then(data => {
    if(data){     

        if(!data.isLive)
            setCountDown(data.eventDate, data.id);
    }
});
