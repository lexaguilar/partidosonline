import http from './http.js';
import { selector, viewMatchNow, viewMatchesNext, setCountDown, tableNext } from './manager.js';

http('live/main').asGet().then(data => {
    if(data){        
        
        let div = selector('matchMain');

        let html = `<h3>${data.teamHome} vs ${data.teamAway}</h3>
            <h4 style="color: grey;">${moment(data.eventDate).calendar()}</h4>
            <img style="width:100%" src="/images/next.jpg" alt="">
            ${data.isLive ? viewMatchNow(data.id) : viewMatchesNext(data.id)}`;

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