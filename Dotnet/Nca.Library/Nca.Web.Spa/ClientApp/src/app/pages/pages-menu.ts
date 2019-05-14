import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'Report 1',
    icon: 'report-icon-1',
    children: [
      {
        title: 'Child 1',
        icon: 'report-icon-2',
        link: '/pages/report/11',
      },
      {
        title: 'Child 2',
        icon: 'report-icon-3',
        link: '/pages/report/12',
      },
    ],
  },
  {
    title: 'Report 2',
    icon: 'report-icon-4',
    link: '/pages/report/2',
  },
  {
    title: 'Report 3',
    icon: 'report-icon-5',
    link: '/pages/report/3',
  },
  {
    title: 'Report 4',
    icon: 'report-icon-6',
    link: '/pages/report/4',
  },
  {
    title: 'Report 5',
    icon: 'report-icon-7',
    link: '/pages/report/5',
  },
  {
    title: 'Report 6',
    icon: 'report-icon-8',
    link: '/pages/report/6',
  },
];
