import { LoggerTemplatePage } from './app.po';

describe('Logger App', function() {
  let page: LoggerTemplatePage;

  beforeEach(() => {
    page = new LoggerTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
