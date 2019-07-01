import { Component } from '@angular/core';

@Component({
  selector: 'nca-chat',
  templateUrl: 'nca-chat.component.html',
  styleUrls: ['nca-chat.component.scss'],
})
export class NcaChatComponent {

  messages: any[] = [];

  constructor() {
  }

  sendMessage(event: any) {
    const files = !event.files ? [] : event.files.map((file) => {
      return {
        url: file.src,
        type: file.type,
        icon: 'nb-compose',
      };
    });

    this.messages.push({
      text: event.message,
      date: new Date(),
      reply: true,
      type: files.length ? 'file' : 'text',
      files: files,
      user: {
        name: 'Jonh Doe',
        avatar: 'https://i.gifer.com/no.gif',
      },
    });
    
  }

  pastePicture(event: any) {
    const items = (event.clipboardData || event.originalEvent.clipboardData).items;
    let blob = null;

    for (const item of items) {
      if (item.type.indexOf('image') === 0) {
        blob = item.getAsFile();
      }
    }
    // load image if there is a pasted image
    if (blob !== null) {
      const reader = new FileReader();
      reader.onload = (evt: any) => {
        console.log(evt.target.result); 
      };
      reader.readAsDataURL(blob);


      this.messages.push({
        text: '<span></span>',
        date: new Date(),
        reply: true,
        type:  'file',
        files: [{
          url: 'https://upload.wikimedia.org/wikipedia/en/thumb/6/63/IMG_%28business%29.svg/1200px-IMG_%28business%29.svg.png',
          type: 'image/jpeg',
          icon: 'file-text-outline',
        }],
        user: {
          name: 'Jonh Doe',
          avatar: 'https://i.gifer.com/no.gif',
        },
      });
    }
  }
}
