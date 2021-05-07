<template>
  <FixedGridPanel>
    <HeaderWrapper>
      <RightHeader />
    </HeaderWrapper>
    <slot />
  </FixedGridPanel>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import FixedGridPanel from '@/ui/GridPanel/FixedGridPanel.vue';
import HeaderWrapper from '@/ui/Header/HeaderWrapper.vue';
import RightHeader from '@/ui/Header/RightHeader.vue';

export default defineComponent({
  components: {
    FixedGridPanel,
    HeaderWrapper,
    RightHeader,
  },
});
</script>

<style scoped></style>
