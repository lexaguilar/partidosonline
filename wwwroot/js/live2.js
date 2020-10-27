import http from './http.js';
import { selector, viewMatchNow, viewMatchesNext, setCountDown, tableNext } from './manager.js';

http('live/main').asGet().then(data => {
    if(data){        
        
        let div = selector('myTime');
        let html = `${moment(data.eventDate).calendar()}`;

        div.innerHTML = html;

        if(!data.isLive)
            setCountDown(data.eventDate, data.id);
    }
});

http('live/next').asGet().then(data => {
    if(data){        
        let div = selector('matchesNext');
        let html = '';
        data.forEach(match =>{
            html +=  tableNext(match);
        });
        div.innerHTML = html;
    }
});