import http from './http.js';
import { selector, tableNext } from './manager.js';

let myId = selector('Id');

http('live/coming/'+myId.value).asGet().then(data => {
    if(data){        
        let div = selector('matchesNext');
        let html = '';
        data.forEach(match =>{
            html +=  tableNext(match);
        });
        div.innerHTML = html;
    }
});