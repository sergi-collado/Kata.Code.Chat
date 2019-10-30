import { Component, OnInit } from '@angular/core';
import { MessageClient, Message } from './chat-api-client.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Kata-Code-Chat-Web';
  constructor(private messageClient: MessageClient) {}
  
  public messages: Message[];
  
  ngOnInit(): void {
    this.messageClient.get()
      .subscribe(
        (messages: Message[]) => {
          this.messages = messages;
        });
  }
}
