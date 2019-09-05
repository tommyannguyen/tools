import { ApiTemplatePage } from './app.po';

describe('Api App', function() {
  let page: ApiTemplatePage;

  beforeEach(() => {
    page = new ApiTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
