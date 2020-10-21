import React from 'react'
import { ResponsiveSunburst } from '@nivo/sunburst'
// make sure parent container have a defined height when using
// responsive component, otherwise height will be 0 and
// no chart will be rendered.
// website examples showcase many properties,
// you'll often use just a few of them.
const MyResponsiveSunburst = ({ data /* see data tab */ }) => (
    <ResponsiveSunburst
        data={data}
        margin={{ top: 40, right: 20, bottom: 20, left: 20 }}
        identity="name"
        value="loc"
        cornerRadius={2}
        borderWidth={1}
        borderColor="white"
        colors={{ scheme: 'nivo' }}
        childColor="noinherit"
        // childColor={{ from: 'color' }}
        animate={true}
        motionStiffness={90}
        motionDamping={15}
        padAngle={0.7}
        isInteractive={true}
        radialLabelsSkipAngle={10}
        radialLabelsTextXOffset={6}
        radialLabelsTextColor="#333333"
        radialLabelsLinkOffset={0}
        radialLabelsLinkDiagonalLength={16}
        radialLabelsLinkHorizontalLength={24}
        radialLabelsLinkStrokeWidth={1}
        radialLabelsLinkColor={{ from: 'color' }}
        slicesLabelsSkipAngle={10}
        slicesLabelsTextColor="#333333"
        animate={true}
        motionStiffness={90}
        motionDamping={15}
        legends={[
            {
                anchor: 'bottom',
                direction: 'row',
                translateY: 56,
                itemWidth: 100,
                itemHeight: 18,
                itemTextColor: '#999',
                symbolSize: 18,
                symbolShape: 'circle',
                effects: [
                    {
                        on: 'hover',
                        style: {
                            itemTextColor: '#000'
                        }
                    }
                ]
            }
        ]}
    />
)

export default MyResponsiveSunburst;